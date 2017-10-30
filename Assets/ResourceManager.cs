using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.Net;
public class ResourceManager
{

    // Use this for initialization
    private MainMonoBehavior mainMonoBehavior;
    private string mResourcePath;
    private Scene mScene;
    private Asset mSceneAsset;
    private static ResourceManager resourceManager = new ResourceManager();//如果没有new 会出现没有实例化的错误
    private Dictionary<string, WWW> wwwCacheMap = new Dictionary<string, WWW>();//这个新添加的原文代码中没有定义它 应该是个字典
    public static ResourceManager getInstance()
    {
        return resourceManager;//静态函数中不能使用非静态成员
    }
    public ResourceManager()
    {
        mainMonoBehavior = GameObject.Find("Main Camera").GetComponent<MainMonoBehavior>();
        mResourcePath = "file://" + Application.dataPath + "/..";
    }

    public void LoadSence(string fileName)
    {
        //Debug.Log(fileName);
        mSceneAsset = new Asset();
        mSceneAsset.Type = Asset.TYPE_JSON;//后面类型判断有用
        mSceneAsset.Source = fileName;
        mainMonoBehavior.UpdateEvent += OnUpdate;//添加监听函数
    }



    public void OnUpdate(GameObject dispatcher)//该函数是监听函数，每帧都会被调用（它的添加是在MainMonoBehavior的start函数中通过调用本地LoadSence函数）
    {
        if (mSceneAsset != null)//表示已经通过了new操作分配类内存空间但是资源还没有加载完
        {
            LoadAsset(mSceneAsset);//这个函数里面会通过判断，使www的new操作只执行一次
            if (!mSceneAsset.isLoadFinished)//C#中 bool类型默认是false
            {
                return;
            }
            mScene = null;
            mSceneAsset = null;
        }
        mainMonoBehavior.UpdateEvent -= OnUpdate;//当所有资源被加载后，删除监听函数。
    }
    //最核心的函数
    private Asset LoadAsset(Asset asset)
    {
        string fullFileName = mResourcePath + "/" + asset.Source;// mResourcePath + "/" + asset.Source;
        Debug.Log("fullFileName=" + fullFileName);
        //if www resource is new, set into www cache
        if (!wwwCacheMap.ContainsKey(fullFileName))
        {//自定义字典 查看开头的定义
            if (asset.www == null)
            {//表示www还没有new操作
                asset.www = new WWW(fullFileName);
                return null;
            }
            if (!asset.www.isDone)
            {
                return null;
            }
            wwwCacheMap.Add(fullFileName, asset.www); //该字典是作为缓存的作用，如果之前已经加载过同样的Unity3D格式文件，那么不需要在加载，直接拿来用就行了。  
        }
        if (asset.Type == Asset.TYPE_JSON)
        { //Json 表示当txt文件被首次加载时的处理
            if (mScene == null)
            {
                string jsonTxt = mSceneAsset.www.text;
                mScene = JsonMapper.ToObject<Scene>(jsonTxt);//mScene是个Asset对象列表，也就是Json文件需要一个AssetList列表对象，注意名字的统一，列表中Asset对象中的成员名称要和txt                //文件中的相关名称统一 不然JsonMapper无法找到
            }
            //load scene
            foreach (Asset sceneAsset in mScene.AssetList)
            {
                if (sceneAsset.isLoadFinished)
                {
                    continue;
                }
                else
                {
                    LoadAsset(sceneAsset);//这里的处理就是 下面 Asset.TYPE_GAMEOBJECT的处理方式，注意是递归函数的调用
                    if (!sceneAsset.isLoadFinished)
                    {
                        return null;
                    }
                }
            }
        }
        else if (asset.Type == Asset.TYPE_GAMEOBJECT)//处理文件中具体信息，嵌套关系或者直接是一个GameObject对象，与上面的代码有联系，Asset创建时候的构造函数中设置成
                                                     //TYPE_GAMEOBJECT类型
        { //Gameobject
            if (asset.gameObject == null)//如果不为null 表示已经通过wwwCacheMap加载了资源包中所包含的资源（fullFileName仅仅是一个文件，资源包中资源是分散的GameObject对象），不需要在重新加载
            {
                NewMethod(fullFileName).assetBundle.LoadAllAssets();//已经通过new WWW操作完成了加载，该函数用来加载包含着资源包中的资源
                GameObject go = (GameObject)GameObject.Instantiate(wwwCacheMap[fullFileName].assetBundle.mainAsset);
                UpdateGameObject(go, asset);
                asset.gameObject = go;
            }
            if (asset.AssetList != null)//有嵌套关系
            {
                foreach (Asset assetChild in asset.AssetList)
                {
                    if (assetChild.isLoadFinished)
                    {
                        continue;
                    }
                    else
                    {
                        Asset assetRet = LoadAsset(assetChild);
                        if (assetRet != null)//这个if else 语句是为了防止你的配置文件中的GameObject对象路径不正确，导致访问空指针。
                        {
                            assetRet.gameObject.transform.parent = asset.gameObject.transform;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        asset.isLoadFinished = true;
        return asset;
    }

    private WWW NewMethod(string fullFileName)
    {
        return wwwCacheMap[fullFileName];
    }

    private void UpdateGameObject(GameObject go, Asset asset)
    {
        //name
        go.name = asset.Name;
        //position
        Vector3 vector3 = new Vector3((float)asset.Position[0], (float)asset.Position[1], (float)asset.Position[2]);
        go.transform.position = vector3;
        //rotation
        vector3 = new Vector3((float)asset.Rotation[0], (float)asset.Rotation[1], (float)asset.Rotation[2]);
        go.transform.eulerAngles = vector3;
    }
}
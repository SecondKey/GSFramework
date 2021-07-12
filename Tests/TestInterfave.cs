using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 一个元件接口
    /// </summary>
    interface IComponent
    {
        /// <summary>
        /// 元件的名称，用于区别于其它元件
        /// </summary>
        string ComponentName { get; }

        /// <summary>
        /// 使用这个元件
        /// <param name="target">对哪个对象使用这个元件</param>
        /// </summary>
        void Use(object target);
    }

    /// <summary>
    /// 一个元件工厂接口
    /// </summary>
    interface IComponentFactory
    {
        /// <summary>
        /// 创建元件
        /// </summary>
        /// <param name="componentName"></param>
        /// <returns></returns>
        IComponent CreateComponent(string componentName);

        /// <summary>
        /// 注册一个元件
        /// </summary>
        /// <param name="component"></param>
        void RegistComponent(IComponent component);
    }

    /// <summary>
    /// 密码锁
    /// </summary>
    class PasswordLocker
    {
        /// <summary>
        /// 开锁用的密码
        /// </summary>
        public string Code { get; set; }
    }

    ///// <summary>
    ///// 钥匙
    ///// </summary>
    //class Key : IComponent
    //{
    //    /// <summary>
    //    /// 对应的解锁密码，只有和锁的Code相同时才可以开锁
    //    /// </summary>
    //    public string Code { get; set; }

    //    public string ComponentName
    //    {
    //        get { return this.Code; }
    //    }

    //    public void Use(object target)
    //    {
    //        //由于入参是继承了接口的Object类型，所以必须先对入参进行类型判断
    //        if (target is PasswordLocker)
    //        {
    //            PasswordLocker pl = (PasswordLocker)target;
    //            if (pl.Code == this.Code) Debug.Log("打开锁了");
    //            else Debug.Log("未能把锁打开");
    //        }
    //        else
    //            Debug.Log("目前类型不是锁，暂时不能使用该元件");

    //    }
    //}

    ///// <summary>
    ///// 钥匙工厂
    ///// </summary>
    //class KeyFactory : IComponentFactory
    //{
    //    private Dictionary<string, IComponent> components = new Dictionary<string, IComponent>();

    //    public IComponent CreateComponent(string componentName)
    //    {
    //        return components[componentName];
    //    }

    //    /// <summary>
    //    /// 由外部创建一个元件
    //    /// </summary>
    //    /// <param name="component"></param>
    //    public void RegistComponent(IComponent component)
    //    {
    //        components[component.ComponentName] = component;
    //    }
    //}

    /// <summary>
    /// 这是一个仅仅能用于开密码锁的元件，使用了现式实现改变入参
    /// </summary>
    class PasswordLockerKey : IComponent
    {
        private string code;

        public PasswordLockerKey(string code)
        {
            this.code = code;
        }

        string IComponent.ComponentName
        {
            get { return this.code; }
        }

        void IComponent.Use(object target)
        {
            PasswordLocker pl = (PasswordLocker)target;
            if (pl.Code == this.code) Debug.Log("打开锁了");
            else Debug.Log("未能把锁打开");
        }

        /// <summary>
        /// 利用显示继承，隐藏了以Object作为入参的方法，并开放了一个仅仅以PasswordLocker作为入参的方法。改变了参数类型
        /// </summary>
        /// <param name="locker"></param>
        public void User(PasswordLocker locker)
        {
            //将自身转化为接口类型，再调用Use才可以使用显式实现的方法
            ((IComponent)this).Use(locker);
        }
    }

    /// <summary>
    /// 基于数据库的钥匙工厂，使用显式实现改变了接口成员的访问权限
    /// </summary>
    class DataBaseKeyFactory : IComponentFactory
    {
        private Dictionary<string, PasswordLockerKey> keys = new Dictionary<string, PasswordLockerKey>();

        /// <summary>
        /// 在构造函数的同时，从数据库中加载出所有钥匙
        /// </summary>
        public DataBaseKeyFactory()
        {
            IComponentFactory f = (IComponentFactory)this;
            foreach (PasswordLockerKey k in LoadKeyFromDatabase())
            {
                f.RegistComponent(k);
            }
        }

        /// <summary>
        /// 这是模拟的通过数据库加载钥匙的方法
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<PasswordLockerKey> LoadKeyFromDatabase()
        {
            return new List<PasswordLockerKey>() { new PasswordLockerKey("12345") };
        }

        IComponent IComponentFactory.CreateComponent(string componentName)
        {
            return keys[componentName];
        }

        void IComponentFactory.RegistComponent(IComponent component)
        {
            keys[component.ComponentName] = (PasswordLockerKey)component;
        }

        /// <summary>
        /// 这里改变了原本接口的返回类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public PasswordLockerKey CreateComponent(string code)
        {
            return (PasswordLockerKey)((IComponentFactory)this).CreateComponent(code);
        }
    }


    class test
    {
        void Test()
        {
            //PasswordLocker locker = new PasswordLocker();
            //locker.Code = "12345";

            //PasswordLockerKey k = new PasswordLockerKey("123456");
            //k.Code = "12345";
            //DataBaseKeyFactory factory = new DataBaseKeyFactory();
            //factory.RegistComponent(k);
            //(factory as IComponentFactory).RegistComponent(k);
            //factory.CreateComponent(locker.Code).Use(locker);
            //IComponent key = new PasswordLockerKey("123456");
            //key.Use(0);
            //PasswordLockerKey k1 = new PasswordLockerKey("123456");
            //k1.User
        }
    }
}
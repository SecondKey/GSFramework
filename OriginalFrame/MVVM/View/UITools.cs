using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GSFramework.MVVM
{
    public static class UITools
    {
        public static IEnumerable GetInsideUI(this GameObject node)
        {
            foreach (Transform t in node.transform)
            {
                if (t.GetComponent<IUILogicalNode>() != null)
                    continue;
                foreach (GameObject g in t.gameObject.GetInsideUI())
                {
                    yield return g;
                }
                yield return t.gameObject;
            }
        }
        /// <summary>
        /// 统一的获取绑定组件的绑定路径的方法
        /// 绑定路径有三种获取方法，根据是否被手动设定来判断使用哪一种路径
        /// 1.使用组件上针对绑定目标设置的具体绑定路径（标准用法）
        /// 2.使用绑定组件的令牌作为路径（目标UI组件的行为或参数较少）
        /// 3.直接使用游戏物体名作为路径（一个游戏物体上只有一个UI组件）
        /// </summary>
        /// <param name="bindingComponent"></param>
        /// <param name="bindingTarget"></param>
        /// <returns></returns>
        public static string GetBindingPath(this UIBindingComponent bindingComponent, string bindingTarget)
        {
            if (bindingComponent.AttributeTokens.Where(p => p.attribute == bindingTarget).Count() > 0)
            {
                return bindingComponent.AttributeTokens.Where(p => p.attribute == bindingTarget).First().path;
            }
            if (!string.IsNullOrEmpty(bindingComponent.componentToken))
            {
                return bindingComponent.componentToken;
            }
            if (!string.IsNullOrEmpty(bindingComponent.gameObject.GetComponent<IUINode>().NodeToken))
            {
                return bindingComponent.gameObject.GetComponent<IUINode>().NodeToken;
            }
            return bindingComponent.gameObject.name;
        }
    }
}
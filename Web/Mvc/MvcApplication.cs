using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using System.Web;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using Oyster.Web.Mvc.Config;
using Oyster.Core.Common;

namespace Oyster.Web.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static bool IsValidActionMethod(MethodInfo methodInfo)
        {
            return !(methodInfo.IsSpecialName ||
                     methodInfo.GetBaseDefinition().DeclaringType.IsAssignableFrom(typeof(Controller)));
        }

        /// <summary>
        /// 必须注册Controller
        /// </summary>
        /// <param name="controller">Controller实例</param>
        public void RegisterController(IController controller)
        {
            if (controller != null)
            {
                Type tp = controller.GetType();
                if (tp != null && !Controllers.ContainsKey(tp.FullName) && tp.Name.EndsWith("Controller"))
                {
                    Controllers.Add(tp.FullName, tp);

                    //Add Action
                    MethodInfo[] allMethods = tp.GetMethods(BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public);
                    MethodInfo[] actionMethods = Array.FindAll(allMethods, IsValidActionMethod);
                    if (actionMethods != null && actionMethods.Length > 0)
                    {
                        foreach (var m in actionMethods)
                        {
                            string mname = m.Name.ToLower();
                            if (!Action.ContainsKey(mname))
                            {
                                Action.Add(mname, new List<string>());
                            }
                            if (!Action[mname].Contains(tp.FullName))
                            {
                                Action[mname].Add(tp.FullName);
                            }

                            string val = tp.FullName + "." + mname;
                            var nm = Helper.GetNameDesc(m);
                            if (!ActionList.ContainsKey(val))
                            {
                                ActionList.Add(val, nm == null ? new NameDesc(val) : nm);
                            }
                        }
                    }
                }
            }
        }

        protected static Dictionary<string, Type> _controllers;
        /// <summary>
        /// 所有控制器-Key:Controller Type.FullName,Value:Controller Type
        /// </summary>
        public static Dictionary<string, Type> Controllers
        {
            get
            {
                if (_controllers == null)
                {
                    _controllers = new Dictionary<string, Type>();
                }
                return _controllers;
            }
        }

        protected static Dictionary<string, List<string>> _actions;
        /// <summary>
        /// 所有的Action,Key:Action Name,Value:所在Controller Type的List
        /// </summary>
        public static Dictionary<string, List<string>> Action
        {
            get
            {
                if (_actions == null)
                {
                    _actions = new Dictionary<string, List<string>>();
                }
                return _actions;
            }
        }

        protected static Dictionary<string, NameDesc> _actionlist;
        public static Dictionary<string, NameDesc> ActionList
        {
            get
            {
                if (_actionlist == null)
                    _actionlist = new Dictionary<string, NameDesc>();
                return _actionlist;
            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            foreach (MvcConfig.RouteClass rc in MvcConfig.Instance.Routes.Values)
            {
                BRoute route2 = new BRoute(rc.Format, new BMvcRouteHandler());
                if (rc.NameSpaces.Count > 0)
                {
                    route2.DataTokens["Namespaces"] = rc.NameSpaces.ToArray();
                }
                if (rc.DefaultParams.Count > 0)
                {
                    route2.Defaults = new RouteValueDictionary(rc.DefaultParams);
                }
                routes.Add(rc.Name, route2);
            }
        }

        protected virtual void Application_Start()
        {
            ConfigEnginer.Instance.Init();

            if (Controllers.Count == 0 || Action.Count == 0)
            {
                throw new Exception("请使用RegisterController Controller,并且保证Controller 中至少存在一个Action.");
            }

            ViewEngines.Engines.Clear();

            ViewEngines.Engines.Add(new MvcViewEngine());

            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(typeof(ControllerFactory));
        }
    }
}

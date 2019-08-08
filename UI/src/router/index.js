import Vue from 'vue'
import Router from 'vue-router'

// in development-env not use lazy-loading, because lazy-loading too many pages will cause webpack hot update too slow. so only in production use lazy-loading;
// detail: https://panjiachen.github.io/vue-element-admin-site/#/lazy-loading

Vue.use(Router)

/* Layout */
import Layout from '../views/layout/Layout'

/**
* hidden: true                   if `hidden:true` will not show in the sidebar(default is false)
* alwaysShow: true               if set true, will always show the root menu, whatever its child routes length
*                                if not set alwaysShow, only more than one route under the children
*                                it will becomes nested mode, otherwise not show the root menu
* redirect: noredirect           if `redirect:noredirect` will no redirect in the breadcrumb
* name:'router-name'             the name is used by <keep-alive> (must set!!!)
* meta : {
    title: 'title'               the name show in submenu and breadcrumb (recommend set)
    icon: 'svg-name'             the icon show in the sidebar,
  }
**/
export const constantRouterMap = [
  { path: '/login', component: () => import('@/views/login/index'), hidden: true },
  { path: '/404', component: () => import('@/views/404'), hidden: true },

  {
    path: '/',
    component: Layout,
    redirect: '/dashboard',
    name: 'dashboard',
    hidden: true,    
    meta: { title: '前端监控', icon: 'example' },
    children: [{
      path: 'dashboard', 
      component: () => import('@/views/monitor/behavior/index')
    }]
  },
  {
    path: '/monitor',
    component: Layout,
    name: 'Monitor',
    meta: { title: '监控统计', icon: 'example' },
    children: [
      {
        path: 'bahavior',
        name: 'Bahavior',
        component: () => import('@/views/monitor/behavior/index'),
        meta: { title: '用户行为', icon: 'table' }
      },
      {
        path: 'customerPv',
        name: 'CustomerPv',
        component: () => import('@/views/monitor/customerPv/index'),
        meta: { title: 'PV', icon: 'table' }
      },
      {
        path: 'loadPage',
        name: 'LoadPage',
        component: () => import('@/views/monitor/loadPage/index'),
        meta: { title: '页面请求', icon: 'table' }
      },
      {
        path: 'httpLog',
        name: 'HttpLog',
        component: () => import('@/views/monitor/httpLog/index'),
        meta: { title: 'HTTP请求', icon: 'table' }
      },
      {
        path: 'jsError',
        name: 'JsError',
        component: () => import('@/views/monitor/jsError/index'),
        meta: { title: 'JS错误', icon: 'table' }
      },
      {
        path: 'resourceLoad',
        name: 'ResourceLoad',
        component: () => import('@/views/monitor/resourceLoad/index'),
        meta: { title: '未加载资源', icon: 'table' }
      },
      {
        path: 'screenShot',
        name: 'ScreenShot',
        component: () => import('@/views/monitor/screenShot/index'),
        meta: { title: '截图', icon: 'table' }
      },
       {
        path: 'extendBehavior',
        name: 'ExtendBehavior',
        component: () => import('@/views/monitor/extendBehavior/index'),
        meta: { title: '自定义行为', icon: 'table' }
      }
    ]
  },
  {
    path: '/system',
    component: Layout,
    name: 'System',
    meta: { title: '系统管理', icon: 'example' },
    children: [
      {
        path: 'user',
        name: 'User',
        component: () => import('@/views/system/user/index'),
        meta: { title: '用户管理', icon: 'table' }
      },
      {
        path: 'project',
        name: 'Project',
        component: () => import('@/views/system/project/index'),
        meta: { title: '项目管理', icon: 'table' }
      }
    ]
  },


  { path: '*', redirect: '/404', hidden: true }
]

export default new Router({
  // mode: 'history', //后端支持可开
  scrollBehavior: () => ({ y: 0 }),
  routes: constantRouterMap
})

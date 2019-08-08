# 一、功能  
* 用户行为：记录用户在页面的操作轨迹，点了哪些菜单、按钮等
* PV：记录访问情况
* 页面请求：查看加载时间，对于慢的页面，进行优化 
* HTTP请求：监控页面的请求，记录请求是否成功
* JS报错：提示出错位置，定位问题
* 未加载资源：记录js、css、image等资源未加载成功
* 截图：对于某些操作，可以截图上传到服务器
* 自定义行为：可以自定义某些操作

# 二、打包  
安装依赖：`npm install`  
webpack打包成一个压缩`monitor.fetch.min.js`，命令：  
`npm run build`

# 三、部署  
需要引入文件`html2canvas.js`和`monitor.fetch.min.js`，公共文件添加对它们的引用，支持自定义事件和截图  
说明：  
* `monitor.fetch.min.js`：记录前端行为  
* `html2canvas.js`：截图功能，如果没用到截图，可以不用添加 
*  截图：  
body截图:`window.monitor.wm_screen_shot("用户查询了系统日志");`  
dom截图：`window.monitor.wm_element_shot($('#id'),"截图");`  
* 自定义行为：`window.monitor.wm_upload_extend_log("zhujy7","movemouse","自定义","USER_MOVE","自定义");`


## 四、 在项目中应用  
*  在`Scripts`文件夹下建一个monitor目录，下面放2个文件`html2canvas.js`和`monitor.fetch.min.js`  

*  公共js开头处添加：  
`document.write("<script language=javascript src='../../../Scripts/monitor/html2canvas.js'></script>");`     
`document.write("<script language=javascript src='../../../Scripts/monitor/monitor.fetch.min.js'></script>");`  
  
*  主页绑定项目ID和用户名：  
`localStorage.setItem('CUSTOMER_WEB_MONITOR_ID', '3E114108-57F6-4A10-AD19-941602ED2335');`     
`localStorage.setItem('CUSTOMER_WEB_USER_ID', '用户ID');`   

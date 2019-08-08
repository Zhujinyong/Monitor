# 前端监控
## 一、功能
* Monitor页面监控    
  * 功能：记录目标页面的用户行为、PV、资源加载、截图、HTTP请求、页面加载、自定义行为等，提交到Api  
  * 技术栈：Webpack、js封装等
  
* Api服务  
  * 功能：Api提供接口，包括上传，分析，用户，项目等  
  * 技术栈：dotnet core webapi、docker、dapper等  
  
* UI管理后台  
  * 功能：查看监控日志，用户管理、项目管理等     
  * 技术栈：vue全家桶、axios、webpack、element-ui等  
   
## 二、页面监控部署  
引入文件`html2canvas.js`和`monitor.fetch.min.js`，公共文件添加对它们的引用，支持自定义事件和截图  
说明：  
[monitor.fetch.min.js](Monitor/libs/monitor.fetch.min.js)：记录前端行为   
[html2canvas.js](Monitor/resource/html2canvas.js)：截图功能，如果没用到截图，可以不用添加   


* 应用  
    1. 在`Scripts`文件夹下建一个monitor目录，下面放2个文件`html2canvas.js`和`monitor.fetch.min.js`  

    2. 公共js开头处添加：  
`document.write("<script language=javascript src='../../../Scripts/monitor/html2canvas.js'></script>");`     
`document.write("<script language=javascript src='../../../Scripts/monitor/monitor.fetch.min.js'></script>");`  
  
    3. 主页绑定项目ID和用户名：  
`localStorage.setItem('CUSTOMER_WEB_MONITOR_ID', '3E114108-57F6-4A10-AD19-941602ED2335');`     
`localStorage.setItem('CUSTOMER_WEB_USER_ID', '用户ID');`   

## 三、效果图   
用户行为      
<img src="/UI/img/behavior.png"   width="600" height="300"  />  

详情      
<img src="/UI/img/behavior-detail.png"   width="600" height="300"  />  

PV   
<img src="/UI/img/pv.png"   width="600" height="300"  />  

详情     
<img src="/UI/img/pv-detail.png"   width="600" height="300"  />  

页面请求     
<img src="/UI/img/page.png"   width="600" height="300"  />  

详情     
<img src="/UI/img/page-detail.png"   width="600" height="300"  />  

HTTP请求    
<img src="/UI/img/http.png"   width="600" height="300"  />  

详情     
<img src="/UI/img/http-detail.png"   width="600" height="300"  />  

js错误     
<img src="/UI/img/js.png"   width="600" height="300"  />  

详情     
<img src="/UI/img/js-detail.png"   width="600" height="300"  />  

未加载资源     
<img src="/UI/img/unload.png"   width="600" height="300"  />  

详情     
<img src="/UI/img/resource-detail.png"   width="600" height="300"  />  

截图     
<img src="/UI/img/shot.png"   width="600" height="300"  />  

详情     
<img src="/UI/img/shot-detail.png"   width="600" height="300"  />  

自定义行为     
<img src="/UI/img/extend.png"   width="600" height="300"  />  

详情     
<img src="/UI/img/extend-detail.png"   width="600" height="300"  />   

项目       
<img src="/UI/img/project.png"   width="600" height="300"  />    

用户       
<img src="/UI/img/user.png"   width="600" height="300"  />  



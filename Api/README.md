# APi
## 一、功能
Api提供接口，包括上传，分析，用户，项目等   
   
## 二、部署    

* 打包镜像  
 `docker build --no-cache -t monitor .`  

*  运行镜像    
`docker run --name=monitor-container -dp 8010:8010 -v /var/monitor:/usr/local/src/monitor/wwwroot/Monitor --restart=always -e LC_ALL="en_US.UTF-8" -e TZ="Asia/Shanghai" -e ASPNETCORE_ENVIRONMENT=Development monitor`     

*  删除镜像    
`docker container rm -f monitor-container`   
  
FROM microsoft/dotnet:2.2-aspnetcore-runtime
 
RUN cd /usr/local/src
 
RUN mkdir monitor
 
WORKDIR /usr/local/src/monitor
 
COPY *.* ./

COPY wwwroot ./wwwroot

Copy runtimes ./runtimes

EXPOSE 8010
 
CMD ["dotnet", "Centa.Monitor.WebAPI.dll"]


<template>
  <div class="app-container">
     <div class="filter-container">
      <el-select v-model="listQuery.webMonitorId" filterable clearable placeholder="项目"  @change="resetPageIndex"  >
      <el-option
      v-for="item in projectList"
      :key="item.keyId"
      :label="item.projectName"
      :value="item.keyId">
    </el-option>
    </el-select>
    <el-input style="width: 200px;"  v-model="listQuery.customerKey" placeholder="用户ID" clearable @change="resetPageIndex"></el-input>
     
    <el-date-picker
      v-model="time"
      type="datetimerange"
      :picker-options="pickerOptions"
      range-separator="至"
      start-placeholder="开始时间"
      end-placeholder="结束时间"
      align="right"
      @change="resetPageIndex" >
    </el-date-picker>
    <el-button type="primary" icon="el-icon-search" @click="getList">查询</el-button>
    </div>

    <el-table  v-loading="listLoading"  border fit highlight-current-row :data="list" style="margin-top:20px;width: 100%">
   <el-table-column align="center" label="序号" width="60"  type="index"
            :index="typeIndex">
       
      </el-table-column>
    <el-table-column
      label="项目" align="center"
      prop="projectName">
    </el-table-column>
     <el-table-column
      label="用户ID" align="center"
      prop="userId">
    </el-table-column>
     <el-table-column
     width="180"
      label="发生时间" align="center"
      prop="happenTime">
    </el-table-column>
    <el-table-column
     width="280"
      label="地址" align="center"
      prop="completeUrl">
       </el-table-column>
    <el-table-column label="描述" width="250" align="center">
          <template slot-scope="scope">
            <span>{{ scope.row|actionMessage }}</span>
          </template>
        </el-table-column>

   
     <el-table-column
      label="操作" align="center"
      width="200">
      <template slot-scope="scope">
        <el-button @click="openDialog(scope.row)"  size="small"  type="primary" >查看</el-button>
      </template>
    </el-table-column>
  </el-table>

  <div class="pagination-container">
      <el-pagination :current-page="listQuery.pageIndex" :page-sizes="[10,20,30,50]" :page-size="listQuery.pageSize" :total="total" layout="total, sizes, prev, pager, next, jumper" @size-change="handleSizeChange" @current-change="handleCurrentChange">
      </el-pagination>
      
    </el-pagination>
    </el-pagination>
  </div>
  


   <el-dialog fullscreen title="明细" :visible.sync="dialogVisible" :close-on-click-modal="false">
       <el-form ref="form" :model="temp" label-width="120px">
     
      <el-form-item label="项目名称">
      <el-input v-model="temp.projectName" disabled></el-input>
      </el-form-item>

      <el-form-item label="用户ID">
      <el-input v-model="temp.userId" disabled></el-input>
      </el-form-item>
<el-form-item label="发生时间">
      <el-input v-model="temp.happenTime" disabled></el-input>
      </el-form-item>
       <el-form-item label="地址">
      <el-input v-model="temp.completeUrl" disabled></el-input>
      </el-form-item>

        <el-form-item label="设备名称">
      <el-input v-model="temp.deviceName" disabled></el-input>
      </el-form-item>
        
        <el-form-item label="操作系统">
      <el-input v-model="temp.os" disabled></el-input>
      </el-form-item>
        <el-form-item label="浏览器名称">
      <el-input v-model="temp.browserName" disabled></el-input>
      </el-form-item>
        <el-form-item label="浏览器版本">
      <el-input v-model="temp.browserVersion" disabled></el-input>
      </el-form-item>
        <el-form-item label="加载类型">
      <el-input v-model="temp.loadType" disabled></el-input>
      </el-form-item>
      <el-form-item label="加载时间">
      <el-input v-model="temp.loadTime" disabled></el-input>
      </el-form-item>
      
       
    
    </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button  type="primary" @click="dialogVisible=false">关闭</el-button>
      </div>
    </el-dialog>
 
 

 </div>

</template>

<script>
import { getProjectList } from '@/api/system/project'
import { getCustomerPVList } from '@/api/monitor/analysis'

import { parseTime } from '@/utils/index'


export default {
  filters: {
  actionMessage: function (value) {
    var message = value.deviceName+','
    +value.os+','
    +value.browserName+' '
    +value.browserVersion;
    return message;
  }
  },
  data() {
    return {
      dialogVisible:false,
      pickerOptions: {
          shortcuts: [{
            text: '最近一周',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
              picker.$emit('pick', [start, end]);
            }
          }, {
            text: '最近一个月',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
              picker.$emit('pick', [start, end]);
            }
          }, {
            text: '最近三个月',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
              picker.$emit('pick', [start, end]);
            }
          }]
        },
      total: null,
      list: null,
      listLoading: true,
      projectList:null,
      time:undefined,
      temp:{
        userId:undefined,
        behaviorType:undefined,
        className:undefined,
        placeholder:undefined,
        inputValue:undefined,
        tagName:undefined,
        innerText:undefined
      },
      listQuery: {
        pageIndex: 1,
        pageSize: 10,
        startTime: undefined,
        endTime: undefined,
        webMonitorId: undefined,
        CustomerKey: undefined
      }
    }
  },
  created() {
    this.fetchData()
  },
  methods: {
      openDialog(row) {
      this.temp=row
      this.dialogVisible = true
    },
     resetPageIndex(v){
      this.listQuery.pageIndex=1
    },
    typeIndex(index) {
        return index + (this.listQuery.pageIndex - 1) * this.listQuery.pageSize + 1;
      },
    getList() {
        this.listLoading = true
        if (this.time!=null&&typeof(this.time) !== "undefined")
        { 
          this.listQuery.startTime=parseTime(this.time[0])
          this.listQuery.endTime=parseTime(this.time[1])
        }  
         else{
          this.listQuery.startTime=''
          this.listQuery.endTime=''
        }
        getCustomerPVList(this.listQuery).then(response => {
        this.list = response.data
        this.listLoading = false
        this.total=response.page.total
        })
    },
    fetchData() {
      getProjectList().then(response => {
             this.projectList = response.data
          })
       this.getList()
      
    },
    handleSizeChange(val) {
      this.listQuery.pageSize = val
      this.fetchData()
    },
    handleCurrentChange(val) {
      this.listQuery.pageIndex = val
      this.fetchData()
    }
  }
}
</script>
<style rel="stylesheet/scss" lang="scss" scoped>

</style>


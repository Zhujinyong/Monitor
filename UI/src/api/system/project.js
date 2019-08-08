import request from '@/utils/request'

export function getProjectList(params) {
  return request({
    url: '/system/project/projects',
    method: 'get',
    params
  })
}



export function addProject(params) {
  return request({
    url: '/system/project',
    method: 'post',
    data: params
  })
}

export function updateProject(params) {
  return request({
    url: '/system/project',
    method: 'put',
    data: params
  })
}



export function deleteProject(id) {
  return request({
    url: '/admin/project',
    method: 'delete',
    data: id
  })
}


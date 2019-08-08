import request from '@/utils/request'

export function getUserList(params) {
  return request({
    url: '/system/user/users',
    method: 'get',
    params
  })
}



export function addUser(params) {
  return request({
    url: '/system/user',
    method: 'post',
    data: params
  })
}

export function updateUser(params) {
  return request({
    url: '/system/user',
    method: 'put',
    data: params
  })
}



export function deleteUser(id) {
  return request({
    url: '/admin/user',
    method: 'delete',
    data: id
  })
}


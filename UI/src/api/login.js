import request from '@/utils/request'

export function login(username, password) {
  return request({
    url: '/system/account/token',
    method: 'post',
    data: {
      account: username,
      password: password
    }
  })
}

export function getInfo(token) {
  return request({
    url: '/system/account/infomation',
    method: 'get'
  })
}

export function logout() {
  return request({
    url: '/system/account/logout',
    method: 'post'
  })
}

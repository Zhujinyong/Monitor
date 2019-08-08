import request from '@/utils/request'

export function getBehaviorList(params) {
  return request({
    url: '/analysis/behaviors',
    method: 'get',
    params
  })
}

export function getCustomerPVList(params) {
  return request({
    url: '/analysis/customer-pvs',
    method: 'get',
    params
  })
}

export function getExtendBehaviorList(params) {
  return request({
    url: '/analysis/extend-behaviors',
    method: 'get',
    params
  })
}

export function getHttpLogList(params) {
  return request({
    url: '/analysis/http-logs',
    method: 'get',
    params
  })
}

export function getJavascriptErrorList(params) {
  return request({
    url: '/analysis/js-errors',
    method: 'get',
    params
  })
}

export function getLoadPageList(params) {
  return request({
    url: '/analysis/load-pages',
    method: 'get',
    params
  })
}

export function getResourceLoadList(params) {
  return request({
    url: '/analysis/load-resources',
    method: 'get',
    params
  })
}

export function getScreenShotList(params) {
  return request({
    url: '/analysis/screen-shots',
    method: 'get',
    params
  })
}
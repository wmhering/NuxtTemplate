import axios from 'axios'
//import { Store } from 'vuex'
import { apiUrl, environment } from '/static/uiConfig'

//Store.commit('config/initialize', environment, apiUrl)
axios.defaults.baseURL = apiUrl

//try {
//  axios.get('account/user')
//    .then(response => {
//      switch (response.status) {
//        case 200:
//          Store.commit('user/setAuthenticated', response.data)
//          break
//        case 401:
//          Store.commit('user/clearAuthenticated')
//          break
//        default:
//          Store.commit('user/clearAuthenticated')
//          // ToDo: Should anything else happen when there is an error code, maybe let the user know
//          break
//        }
//      }
//    )
//} catch (e) {
//  Store.commit('user/clearAuthenticated')
//  // ToDo: Should anything else happen when there is an error, maybe let the user know
//}

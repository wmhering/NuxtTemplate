export const state = () => ({
  userName: '',
  lastName: '',
  email: '',
  employeeKey: 0,
  permissions: [],
  authenticated: false
})

export const mutations = {
  clearAuthenticated (state) {
    state.userName = ''
    state.lastName = ''
    state.email = ''
    state.employeeKey = 0
    state.permissions = []
    state.authenticated = false
  },
  setAuthenticated (state, data) {
    state.userName = data.userName
    state.lastName = data.lastName
    state.email = data.email
    state.employeeKey = data.employeeKey
    state.permissions = data.permissions
    state.authenticated = true
  }
}

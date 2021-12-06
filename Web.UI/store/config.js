export const state = () => ({
  environment: '',
  apiUrl: ''
})

export const mutations = {
  initialize (state, environment, apiUrl) {
    state.environment = environment
    state.apiUrl = apiUrl
  }
}

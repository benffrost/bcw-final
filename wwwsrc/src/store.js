import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'
import AuthService from './AuthService'

Vue.use(Vuex)

let baseUrl = location.host.includes('localhost') ? '//localhost:5000/' : '/'

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
})

export default new Vuex.Store({
  state: {
    user: {},
    publicKeeps: [],
    privateKeeps: [],
    privateVaults: [],
    vaultKeeps: []
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    resetState(state) {
      //clear the entire state object of user data
      state.user = {}
    },
    setPublicKeeps(state, keeps) { state.publicKeeps = keeps; },
    setPrivateKeeps(state, keeps) { state.privateKeeps = keeps; },
    setPrivateVaults(state, vaults) { state.privateVaults = vaults; },
    setVaultKeeps(state, keeps) { state.vaultKeeps = keeps; },
    deleteKeep(state, keepId) {
      let index1 = state.publicKeeps.findIndex(el => el.id == keepId);
      let index2 = state.privateKeeps.findIndex(el => el.id == keepId);
      let index3 = state.vaultKeeps.findIndex(el => el.id == keepId);

      if (index1 >= -1) {
        state.publicKeeps.splice(index1, 1)
      }
      if (index2 >= 0) {
        state.privateKeeps.splice(index2, 1)
      }
      if (index3 >= 0) {
        state.vaultKeeps.splice(index3, 1)
      }
    },
    deleteVault(state, vaultId) {
      let index = state.privateVaults.findIndex(el => el.id == vaultId);
      if (index >= -1) {
        state.privateVaults.splice(index, 1)
      }
    },
    pushKeep(state, keep) {
      state.publicKeeps.push(keep);
      state.privateKeeps.push(keep);
    },
    pushVault(state, vault) {
      state.privateVaults.push(vault);
    },
    pushKeepVault(state, keepvault) {
      let index = state.publicKeeps.findIndex(el => el.id == keepvault.keepId);
      if (index >= -1) {
        state.vaultKeeps.push(state.publicKeeps[index])
      }
    },
    spliceKeepVault(state, keepvault) {
      let index = state.vaultKeeps.findIndex(el => el.id == keepvault.keepId);
      if (index >= -1) {
        state.vaultKeeps.splice(index, 1)
      }
    }
  },
  actions: {
    async register({ commit, dispatch }, creds) {
      try {
        let user = await AuthService.Register(creds)
        commit('setUser', user)
        router.push({ name: "home" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    async login({ commit, dispatch }, creds) {
      try {
        let user = await AuthService.Login(creds)
        commit('setUser', user)
        router.push({ name: "home" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    async logout({ commit, dispatch }) {
      try {
        let success = await AuthService.Logout()
        if (!success) { }
        commit('resetState')
        router.push({ name: "login" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    async getPublicKeeps({ commit, dispatch }) {
      api.get('keeps')
        .then(res => commit('setPublicKeeps', res.data))
        .catch(error => console.error(error))
    },
    async getPrivateKeeps({ commit, dispatch }) {
      api.get('keeps/user')
        .then(res => commit('setPrivateKeeps', res.data))
        .catch(error => console.error(error))
    },
    async getPrivateVaults({ commit, dispatch }) {
      api.get('vaults')
        .then(res => commit('setPrivateVaults', res.data))
        .catch(error => console.error(error))
    },
    async getVaultKeeps({ commit, dispatch }, vaultId) {
      api.get('vaultkeeps/' + vaultId)
        .then(res => commit('setVaultKeeps', res.data))
        .catch(error => console.error(error))
    },
    async deleteKeep({ commit, dispatch }, keepId) {

      api.delete('keeps/' + keepId)

        .then(res => commit('deleteKeep', keepId))
        .catch(error => console.error(error))
    },
    async addKeep({ commit, dispatch }, keep) {
      api.post('keeps', keep)
        .then(res => {
          commit('pushKeep', keep)
          router.push('dash')
        })
        .catch(error => console.error(error))
    },
    async addVault({ commit, dispatch }, vault) {
      api.post('vaults', vault)
        .then(res => {
          commit('pushVault', vault)
          router.push('dash')
        })
        .catch(error => console.error(error))
    },
    async deleteVault({ commit, dispatch }, vaultId) {
      api.delete('vaults/' + vaultId)
        .then(res => commit('deleteVault', vaultId))
        .catch(error => console.error(error))
    },
    async addKeepToVault({ commit, dispatch, state }, keepvault) {
      let index = state.vaultKeeps.findIndex(el => el.id == keepvault.keepId)

      if (index == -1) {
        api.post('vaultkeeps', keepvault)
          .then(res => commit('pushKeepVault', keepvault))
          .catch(error => console.error(error))
      }
    },
    async removeKeepFromVault({ commit, dispatch }, keepvault) {
      api.put('vaultkeeps', keepvault)
        .then(res => commit('spliceKeepVault', keepvault))
        .catch(error => console.error(error))
    },
  }
})

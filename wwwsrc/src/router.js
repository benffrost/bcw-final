import Vue from 'vue'
import Router from 'vue-router'
// @ts-ignore
import Home from './views/Home.vue'
// @ts-ignore
import Login from './views/Login.vue'
import Dash from './views/Dash.vue'
import VaultContents from './views/VaultContents.vue'
import KeepContents from './views/VaultContents.vue'
import CreateKeep from './views/CreateKeep.vue'
import CreateVault from './views/CreateVault.vue'
Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/dash',
      name: 'dash',
      component: Dash
    },
    {
      path: '/vault/:vaultId',
      name: 'VaultContents',
      component: VaultContents,
      props: true
    },
    {
      path: '/keep/:keepId',
      name: 'KeepContents',
      component: KeepContents,
      props: true
    },
    {
      path: '/newKeep',
      name: 'CreateKeep',
      component: CreateKeep,
    },
    {
      path: '/newVault',
      name: 'CreateVault',
      component: CreateVault,
    }

  ]
})

<template>
  <div id="app">
    <div class="container-fluid">
      <div class="row border-bottom">
        <div class="col-2">
          <router-link :to="{name: 'home'}">
            <h4>KEEPR</h4>
          </router-link>
        </div>
        <div class="col-2">
          <router-link v-if="user.id" :to="{name: 'dash'}">
            <h4>DASHBRD</h4>
          </router-link>
        </div>
        <div class="col-2">
          <router-link v-if="user.id" :to="{name: 'CreateKeep'}">
            <h4>NEWKEEP</h4>
          </router-link>
        </div>
        <div class="col-3">
          <router-link v-if="user.id" :to="{name: 'CreateVault'}">
            <h4>NEWVAULT</h4>
          </router-link>
        </div>

        <div class="col-2">
          <router-link v-if="user.id" :to="{name: 'login'}" @click.native="logout">
            <h4>LOGOUT</h4>
          </router-link>
          <router-link v-else :to="{name: 'login'}">
            <h4>LOGIN</h4>
          </router-link>
        </div>
      </div>
      <router-view />
    </div>
  </div>
</template>

<script>
  export default {
    computed: {
      user() {
        return this.$store.state.user;
      }
    },
    methods: {
      logout() {
        this.$store.dispatch("logout");
      }
    }
  }
</script>
<style>
  #app {
    font-family: "Avenir", Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
  }

  #nav {
    padding: 30px;
  }

  #nav a {
    font-weight: bold;
    color: #2c3e50;
  }

  #nav a.router-link-exact-active {
    color: #42b983;
  }
</style>
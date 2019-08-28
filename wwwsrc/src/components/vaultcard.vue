<template>
  <div class="vaultcard card m-2">

    <div class="card-body">
      <h5 class="card-title">{{vault.name}}</h5>
      <p class="card-text">{{vault.description}}</p>

      <div class="card-footer">
        <router-link :to="{name: 'VaultContents', params: {vaultId: vault.id}}"><button>View</button></router-link>
        <button @click="deleteVault()">delete</button>
      </div>
    </div>

  </div>
</template>


<script>
  export default {
    name: 'vaultcard',
    props: ['vault'],
    data() {
      return {}
    },
    computed: {},
    methods: {
      deleteVault() {
        this.$store.dispatch("deleteVault", this.vault.id)
      }
    },
    components: {}
  }
</script>


<style scoped>

</style>
<template>
  <div class="createvault mt-2">
    <form @submit.prevent="addVault">
      <input type="text" placeholder="name" v-model='newVault.name' required>
      <input type="text" placeholder="description" v-model='newVault.description' required>
      <button class="btn btn-success btn-sm" type="submit">Add Vault</button>
    </form>


  </div>
</template>


<script>
  export default {
    name: 'createvault',
    data() {
      return {
        newVault: {
          name: "",
          description: "",
          img: "",
          isPrivate: false
        }
      }
    },
    computed: {},
    methods: {
      addVault() {
        this.$store.dispatch("addVault", this.newVault);
      }
    },
    components: {}
  }
</script>


<style scoped>

</style>
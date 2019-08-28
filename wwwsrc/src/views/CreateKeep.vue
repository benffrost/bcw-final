<template>
  <div class="createkeep mt-2">
    <form @submit.prevent="addKeep">
      <input type="text" placeholder="name" v-model='newKeep.name' required>
      <input type="text" placeholder="description" v-model='newKeep.description' required>
      <input type="text" placeholder="image url" v-model='newKeep.img' required>

      <button class="btn btn-success btn-sm" type="submit">Add Keep</button>
    </form>


  </div>
</template>


<script>
  export default {
    name: 'createkeep',
    data() {
      return {
        newKeep: {
          name: "",
          description: "",
          img: "",
          isPrivate: false
        }
      }
    },
    computed: {},
    methods: {
      addKeep() {
        this.$store.dispatch("addKeep", this.newKeep);
      }
    },
    components: {}
  }
</script>


<style scoped>

</style>
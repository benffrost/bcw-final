<template>
  <div class="keepcard card m-2">
    <div class="card-body">
      <h5 class="card-title">{{keep.name}}</h5>
      <p class="card-text">{{keep.description}} - Kept: {{keep.keeps}}</p>
      <div class="card-footer">
        <button v-if="user.id && user.id == keep.userId" @click="deleteKeep()">delete</button>
      </div>
    </div>

  </div>
</template>


<script>
  export default {
    name: 'keepcard',
    props: ['keep'],
    data() {
      return {}
    },
    computed: {
      user() {
        return this.$store.state.user;
      }
    },
    methods: {
      deleteKeep() {
        this.$store.dispatch("deleteKeep", this.keep.id)
      }
    },
    components: {}
  }
</script>


<style scoped>

</style>
<template>
  <div class="dash">
    <div class="row">
      <div class="col">
        <h3>VAULTS:</h3>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <myvaults :vaults="privateVaults"></myvaults>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <div class="row">
          <div class="col">
            <h3>KEEPS:</h3>
          </div>
        </div>
        <mykeeps :keeps="privateKeeps"></mykeeps>
      </div>
    </div>
  </div>
</template>


<script>
  import myvaults from '../components/myvaults.vue'
  import mykeeps from '../components/mykeeps.vue'

  export default {
    name: 'dash',
    data() {
      return {}
    },
    components: { myvaults, mykeeps },
    computed: {
      privateVaults() {
        return this.$store.state.privateVaults;
      },
      privateKeeps() {
        return this.$store.state.privateKeeps;
      }

    },
    mounted() {
      this.$store.dispatch("getPrivateKeeps");
      this.$store.dispatch("getPrivateVaults");
    },
    methods: {},
  }
</script>


<style scoped>

</style>
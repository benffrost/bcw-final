<template>
  <div class="home d-flex">
    <keepcard v-for="keep in publicKeeps" :keep="keep"></keepcard>

  </div>
</template>

<script>
  import keepcard from "../components/keepcard.vue"
  export default {
    name: "home",

    components: { keepcard },
    computed: {
      publicKeeps() {
        if (this.$store.state.publicKeeps[0]) {
          return this.$store.state.publicKeeps;
        }
        else return []

      }
    },
    mounted() {
      this.$store.dispatch("getPublicKeeps");
    },
    methods: {
    }
  };
</script>
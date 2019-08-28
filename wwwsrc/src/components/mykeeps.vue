<template>
  <div class="mykeeps d-flex">
    <keepcard v-for="keep in keeps" :keep="keep"></keepcard>

  </div>
</template>

<script>
  import keepcard from "../components/keepcard.vue"
  export default {
    name: "mykeeps",

    components: { keepcard },
    props: ['keeps']


  };
</script>
<template>
  <div class="myvaults d-flex">
    <vaultcard v-for="vault in vaults" :vault="vault"></vaultcard>
  </div>
</template>

<script>
  import vaultcard from "../components/vaultcard.vue"
  export default {
    name: "myvaults",
    components: { vaultcard },
    props: ['vaults']
  };
</script>
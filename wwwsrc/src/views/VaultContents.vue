<template>
  <div class="VaultContents">
    <div class="row">
      <div class="col">
        <h3>KEEPS IN VAULT:</h3>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <div v-for="keep in vaultKeeps" @click="removeKeepFromVault(keep.id)">
          <keepcard :keep="keep"></keepcard>
        </div>
      </div>
    </div>

    <div class="row">
      <div class="col">
        <h3>AVAILABLE KEEPS:</h3>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <div v-for="keep in publicKeeps" @click="addKeepToVault(keep.id)">
          <keepcard :keep="keep"></keepcard>
        </div>

      </div>
    </div>
  </div>
</template>


<script>
  import keepcard from '../components/keepcard.vue'

  export default {
    name: 'VaultContents',
    data() {
      return {}
    },
    props: ['vaultId'],
    components: { keepcard },
    computed: {
      vaultKeeps() {
        return this.$store.state.vaultKeeps;
      },
      publicKeeps() {
        return this.$store.state.publicKeeps;
      }
    },
    mounted() {
      this.$store.dispatch("getVaultKeeps", this.vaultId);
      this.$store.dispatch("getPublicKeeps");
    },
    methods: {
      addKeepToVault(keepId) {
        this.$store.dispatch("addKeepToVault", { keepId, vaultId: this.vaultId });
      },
      removeKeepFromVault(keepId) {
        this.$store.dispatch("removeKeepFromVault", { keepId, vaultId: this.vaultId });

      }
    },
  }
</script>


<style scoped>

</style>
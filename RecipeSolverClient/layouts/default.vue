<template>
  <v-container fluid>
    <OverheadBar />
    <div class="page-content">
      <nuxt />
    </div>
  </v-container>
</template>

<script>
import OverheadBar from '~/components/OverheadBar.vue';

export default {
  components: { OverheadBar },
}
</script>
<style>
.page-content {
  padding-top: 80px; 
}
.v-application--wrap {
  flex: 1 1 auto !important;
  backface-visibility: hidden !important;
  display: flex !important;
  flex-direction: column !important;
  min-height: 0  !important;
  max-width: 100% !important;
  position: relative !important;
}
</style>
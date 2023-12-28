<template>

  <v-container>

   
    <OverheadBar />
   
    <nuxt />
 
  </v-container>

</template>

<script>
import OverheadBar from '~/components/OverheadBar.vue';
export default {
  components: {OverheadBar},
  data() {
    return {};
  },
  computed: {},
  mounted() {},
  methods: {}
};
</script>
<style>
</style>
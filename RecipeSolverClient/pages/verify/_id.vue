<template>
  <div class="page-container">
    <v-container class="content-container">
      <div v-if="isLoading" class="loader">
        <v-progress-circular indeterminate color="primary"></v-progress-circular>
      </div>
      <div v-else class="centered-content">
        <p v-if="verificationSuccess">
          Adres email został zweryfikowany. Możesz się teraz zalogować.
          <router-link to="/login">
            <button>Zaloguj się</button>
          </router-link>
        </p>
        <p v-else-if="verificationError">
          Wystąpił błąd podczas weryfikacji adresu email.
        </p>
      </div>
    </v-container>
  </div>
</template>


<script>
export default {
  layout: 'auth',
  auth: false,
  data() {
    return {
      isLoading: true,
      verificationSuccess: false,
      verificationError: false,
    };
  },
  head() {
    return {
      title: 'Weryfikacja',
    };
  },
  async created() {
    try {
      const verificationId = this.$route.params.id;
      await this.verifyEmail(verificationId);
      this.verificationSuccess = true;
    } catch (error) {
      console.error('Verification error:', error);
      this.verificationError = true;
    } finally {
      this.isLoading = false;
    }
  },
  methods: {
    async verifyEmail(verificationId) {
      try {
        await this.$axios.get(`/api/Auth/Verify/${verificationId}`);
      } catch (error) {
        console.log(error);
        throw error;
      }
    },
  },
};
</script>

<style lang="scss" scoped>
@import '@/assets/auth.scss';
</style>

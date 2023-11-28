<template>
    <v-container>
      <p v-if="verificationSuccess">
        Adres email został zweryfikowany. Możesz się teraz zalogować.
      </p>
      <p v-else-if="verificationError">
        Wystąpił błąd podczas weryfikacji adresu email.
      </p>
    </v-container>
  </template>
  
  <script>
  export default {
    layout: 'auth',
    data() {
      return {
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
      const verificationId = this.$route.params.id;
      try {
        await this.verifyEmail(verificationId);
        this.verificationSuccess = true;
      } catch (error) {
        console.error('Verification error:', error);
        this.verificationError = true;
      }
    },
    methods: {
      async verifyEmail(verificationId) {
        try {
          await this.$axios.get(`/api/Auth/Verify/${verificationId}`);
        } catch (error) {
          console.log(error)
        }
      },
    },
  };
  </script>
  
  <style scoped>
  @import '@/assets/auth.scss';
  </style>
  
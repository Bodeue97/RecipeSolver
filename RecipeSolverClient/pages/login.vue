<template>
  <v-container>
    <v-form class="custom-form" @submit.prevent="login">
      <label for="email">Email:</label>
      <input type="email" id="email" v-model="email" required /><br /><br />

      <label for="password">Hasło:</label>
      <input
        type="password"
        id="password"
        v-model="password"
        required
      /><br /><br />

      <button type="submit">Zaloguj się</button>

      <!-- Include ValidationErrorMessage component to display validation errors -->
      <ValidationErrorMessage
        :validationMessage="validationMessage"
        :showPopup="showPopup"
      />
    </v-form>
  </v-container>
</template>
  
  <script>
import * as validate from '@/plugins/validation.js'
import ValidationErrorMessage from '~/components/ValidationErrorMessage.vue'

export default {
  auth: false,
  layout: 'auth',
  components: {
    ValidationErrorMessage,
  },
  data() {
    return {
      email: '',
      password: '',
      validationMessage: '',
      showPopup: false,
    }
  },
  head() {
    return {
      title: 'Logowanie',
    }
  },
  methods: {
    async login() {
      const { email, password } = this

      this.validationMessage = ''
      this.showPopup = false

      // Validate email
      if (!validate.validateEmail(email)) {
        this.validationMessage = 'Niepoprawny adres email'
        this.showPopup = true
        this.hideErrorMessageAfterDelay()
        return
      }

      // Validate password
      if (!validate.validatePassword(password)) {
        this.validationMessage =
          'Hasło musi składać się z przynajmniej 8 znaków'
        this.showPopup = true
        this.hideErrorMessageAfterDelay()
        return
      }

      try {
        this.$auth
          .loginWith('local', {
            data: { email, password },
          })
          .then((response) => {
            // Access the response data here
            const { user } = response.data
            
            this.$auth.$storage.setUniversal('user', user, true);
            // Handle the user and token data as needed
            

            // Redirect to home page or perform other actions
            window.location.reload(true);
          })
          .catch((error) => {
            // Handle login errors
            console.error('Login error:', error)
          })

        this.$router.push('/')
      } catch (error) {
        console.error('Error logging in:', error)

        // Display error message in the popup
        this.validationMessage = error.message || 'Błąd logowania'
        this.showPopup = true
        this.hideErrorMessageAfterDelay()
      }
    },

    hideErrorMessageAfterDelay() {
      setTimeout(() => {
        this.validationMessage = ''
        this.showPopup = false
      }, 2000)
    },
  },
}
</script>

<style lang="scss" scoped>
@import '@/assets/auth.scss';
</style>
  
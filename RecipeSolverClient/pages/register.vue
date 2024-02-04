<template>
    <v-container>
      <v-form @submit.prevent="submitForm" v-if="!formSent">
        <label for="email">Email:</label>
        <input
          type="email"
          id="email"
          v-model="formData.email"
          required
        /><br /><br />
  
        <label for="password">Hasło:</label>
        <input
          type="password"
          id="password"
          v-model="formData.password"
          required
        /><br /><br />
  
        <label for="confirmPassword">Powtórz hasło:</label>
        <input
          type="password"
          id="confirmPassword"
          v-model="formData.confirmPassword"
          required
        /><br /><br />
  
        <label for="name">Imię:</label>
        <input type="text" id="name" v-model="formData.name" required /><br /><br />
  
        <label for="surname">Nazwisko:</label>
        <input
          type="text"
          id="surname"
          v-model="formData.surname"
          required
        /><br /><br />
  
        <button type="submit">Zarejestruj się</button>
      </v-form>
      <p v-if="formSent">{{ successMessage }}</p>
      <ValidationErrorMessage
        :validationMessage="validationMessage"
        :showPopup="showPopup"
      />
  
      <router-link v-if="formSent" to="/login">
        <button>Zaloguj się</button>
      </router-link>
    </v-container>
  </template>
  
  <script>
  import * as validate from '@/plugins/validation.js'
  import ValidationErrorMessage from '~/components/ValidationErrorMessage.vue'
  
  export default {
    components: {
      ValidationErrorMessage,
    },
    auth: false,
    layout: 'auth',
    data() {
      return {
        formData: {
          email: '',
          password: '',
          confirmPassword: '',
          name: '',
          surname: '',
        },
        validationMessage: '',
        showPopup: false,
        formSent: false,
        successMessage: '',
      }
    },
    head() {
      return {
        title: 'Rejestracja',
      }
    },
    methods: {
      async submitForm() {
        const { email, password, confirmPassword, name, surname } = this.formData
  
        this.validationMessage = ''
        this.showPopup = false
  
        if (!validate.validateEmail(email)) {
          this.validationMessage = 'Niepoprawny adres email'
          this.showPopup = true
          this.hideErrorMessageAfterDelay()
          return
        }
  
        if (!validate.validatePassword(password)) {
          this.validationMessage = 'Hasło musi składać się z przynajmniej 8 znaków'
          this.showPopup = true
          this.hideErrorMessageAfterDelay()
          return
        }
  
        if (!validate.validateConfirmPassword(password, confirmPassword)) {
          this.validationMessage = 'Pola "Hasło" i "Powtórz hasło" różnią się'
          this.showPopup = true
          this.hideErrorMessageAfterDelay()
          return
        }
  
        if (!name.trim()) {
          this.validationMessage = 'Pole "Imię" nie może być puste'
          this.showPopup = true
          this.hideErrorMessageAfterDelay()
          return
        }
  
        if (!surname.trim()) {
          this.validationMessage = 'Pole "Nazwisko" nie może być puste'
          this.showPopup = true
          this.hideErrorMessageAfterDelay()
          return
        }
  
        try {
          await this.$axios.post('/api/Auth/Register', {
            email,
            password,
            confirmPassword,
            name,
            surname,
          })
  
          this.formSent = true
          this.successMessage =
            'Wysłano wiadomość email z linkiem weryfikacyjnym. Sprawdź swoją skrzynkę pocztową.'
        } catch (error) {
          console.error('Error sending registration request:', error)
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
  
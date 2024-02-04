<template>
    <div>
      <h1>Usuwanie użytkowników</h1>
      <v-data-table class="centered-cells" :headers="headers" :items="users" :search="search">
        <template v-slot:[`item.actions`]="{ item }">
          <v-icon @click="deleteItem(item.user.id)">mdi-delete</v-icon>
        </template>
      </v-data-table>
      <ValidationErrorMessage :validationMessage="validationMessage" :showPopup="showPopup" />
    </div>
  </template>
  
  <script>
  import ValidationErrorMessage from '~/components/ValidationErrorMessage.vue'
  export default {
    auth: true,
    layout: 'default',
    components: {
      ValidationErrorMessage,
    },
    data() {
      return {
        user: this.$auth.$storage.getUniversal('user'),
        validationMessage: '',
        showPopup: false,
        users: [],
        headers: [
          { text: 'Działania', value: 'actions', align: 'center', sortable: false },
          { text: 'ID', value: 'user.id', align: 'right' },
          { text: 'Email', value: 'user.email', align: 'right' },
          
  
        ],
        search: '',
      }
    },
    head() {
      return {
        title: 'Usuwanie użytkowników',
      }
    },
    computed: {
  
    },
    methods: {
      async getUsers() {
        try {
         
          const response = await this.$axios.get(
            `/api/Auth/GetAll`
          )
  
          this.users = response.data.map((item) => ({
            
            user: {
              id: item.id,
              email: item.email,
            },
          }))
        } catch (error) {
          console.error('Błąd przy pobieraniu użytkowników:', error)
  
          this.showPopup = true
          this.validationMessage = error.message
          this.hideErrorMessageAfterDelay()
        }
      },
     
     
  
      async deleteItem(id) {
        try {
          const request = await this.$axios.delete(`/api/Auth/Delete/${id}`)
          this.getUsers();
        } catch (error) {
  
          this.showPopup = true
          this.validationMessage = error.message
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
    async mounted() {
      this.getUsers();
    },
  }
  </script>
  
  <style lang="scss" scoped>
  @import '@/assets/pantry.scss';
  
  .centered-cells .v-data-table-body td {
    width: 100px !important;
  }
  </style>
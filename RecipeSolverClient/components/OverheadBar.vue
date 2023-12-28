<template>
  <v-app style="background-color: rgba(255, 255, 255, 0)">
    <v-navigation-drawer v-model="drawer" app right class="sidebar-menu">
      <v-list v-if="!user || !isLoggedIn" class="vertical-list">
        <v-list-item>
          <v-btn to="/login" class="list-item-button">Zaloguj się</v-btn>
        </v-list-item>
        <v-list-item>
          <v-btn to="/register" class="list-item-button">Rejestracja</v-btn>
        </v-list-item>
      </v-list>
      <v-list v-if="user && isLoggedIn" class="vertical-list">
        <v-list-item> Witaj {{ user.name }} </v-list-item>
        <v-list-item>
          <v-btn to="/pantry" class="list-item-button">Spiżarnia</v-btn>
        </v-list-item>
        <v-list-item>
          <v-btn to="/my-recipes" class="list-item-button">Moje przepisy</v-btn>
        </v-list-item>
        <v-list-item>
          <v-btn to="/recipe-create" class="list-item-button"
            ><v-icon>mdi-plus-circle</v-icon>Stwórz przepis</v-btn
          >
        </v-list-item>
        <v-list-item>
          <v-btn @click="logout" class="list-item-button"
            ><v-icon>mdi-logout</v-icon>Wyloguj</v-btn
          >
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-container>
      <v-toolbar
        :class="{ 'toolbar-offset': drawer, 'toolbar-not-offset': !drawer }"
        class="overhead-bar"
      >
        <v-spacer></v-spacer>
        <v-btn
          style="color: rgb(158, 156, 156); background-color: #c0680497"
          @click="toggleDrawer"
          text
        >
          Menu
          <v-icon>mdi-menu</v-icon>
        </v-btn>
      </v-toolbar>
    </v-container>
  </v-app>
</template>

<script>
export default {
  data() {
    return {
      isLoggedIn: false,
      user: null,
      drawer: false,
    }
  },
  created() {
    this.isLoggedIn = this.$auth.loggedIn
    this.user = this.$auth.$storage.getUniversal('user')

    this.$watch('$auth.loggedIn', (loggedIn) => {
      this.isLoggedIn = loggedIn
      this.user = this.$auth.$storage.getUniversal('user')
    })
  },
  methods: {
    logout() {
      this.$auth
        .logout()
        .then(() => {
          this.$auth.$storage.removeUniversal('user', null, true)

          // Check if the user is logged out
          if (!this.$auth.loggedIn) {
            if (this.$route.path !== '/') {
              this.$router.push('/')
            }
          } else {
            // Handle the case where logout was not successful
            console.error('Logout unsuccessful')
          }
        })
        .catch((error) => {
          console.error('Error during logout:', error)
          // Handle logout error if needed
        })
    },
    toggleDrawer() {
      this.drawer = !this.drawer
      console.log(this.user)
    },
  },
}
</script>

<style lang="scss" scoped>
@import '@/assets/overhead-bar.scss';
</style>

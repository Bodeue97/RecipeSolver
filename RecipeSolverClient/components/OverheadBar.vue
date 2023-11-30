<template>
  <v-app style="background-color: rgba(255,255,255,0);">
    <v-navigation-drawer v-model="drawer" app>
      <!-- Contents of the sidebar -->
      <v-list>
        <v-list-item v-if="!user" class="no-user-bar">
          <v-btn to="/login" class="toolbar-btn">Zaloguj się</v-btn>
          <v-btn to="/register" class="toolbar-btn">Rejestracja</v-btn>
        </v-list-item>
        <v-list-item v-if="user" class="user-info">
          <v-toolbar-title>{{ user.name }}</v-toolbar-title>
          <v-btn to="/pantry" class="toolbar-btn">Spiżarnia</v-btn>
          <v-btn to="/my-recipes" class="toolbar-btn">Moje przepisy</v-btn>
          <v-btn to="/recipe-create" class="toolbar-btn">Stwórz przepis</v-btn>
          <v-btn class="toolbar-btn" @click="logout">Wyloguj</v-btn>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    
    <v-container>
      <v-toolbar class="overhead-bar">
        <!-- Button to toggle the sidebar -->
        <v-btn style="color: rgb(158, 156, 156)" @click="drawer = !drawer" text>
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
      drawer: false 
    };
  },
  created() {
    this.isLoggedIn = this.$auth.loggedIn;
    this.user = this.$auth.$storage.getUniversal("user");

    this.$watch('$auth.loggedIn', (loggedIn) => {
      this.isLoggedIn = loggedIn;
      this.user = this.$auth.$storage.getUniversal("user");
    });
  },
  methods: {
    logout() {
      this.$auth.logout();
      this.$router.push("/login");
    },
  },
};
</script>

<style lang="scss">
// @import '@/assets/overhead-bar.scss';  
</style>

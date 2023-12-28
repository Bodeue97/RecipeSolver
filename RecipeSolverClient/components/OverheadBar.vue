<template>
  <v-app style="background-color: rgba(255, 255, 255, 0)">
    <v-navigation-drawer v-model="drawer" app right>
      <v-list v-if="!user || !isLoggedIn">
        <v-list-item>
          <v-btn to="/login">Zaloguj się</v-btn>
        </v-list-item>
        <v-list-item>
          <v-btn to="/register">Rejestracja</v-btn>
        </v-list-item>
      </v-list>
      <v-list v-if="user && isLoggedIn">
        <v-list-item> Witaj {{ user.name }} </v-list-item>
        <v-list-item>
          <v-btn to="/pantry">Spiżarnia</v-btn>
        </v-list-item>
        <v-list-item>
          <v-btn to="/my-recipes">Moje przepisy</v-btn>
        </v-list-item>
        <v-list-item>
          <v-btn to="/recipe/create"
            ><v-icon>mdi-plus-circle</v-icon>Stwórz przepis</v-btn
          >
        </v-list-item>
        <v-list-item>
          <v-btn @click="logout"><v-icon>mdi-logout</v-icon>Wyloguj</v-btn>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <div class="bar-wrapper">
    <v-app-bar
      fixed
      :class="{ 'retracted': drawer , 'sticky-header': isHeaderSticky }"
      class="overhead-bar"
      >
      <v-spacer></v-spacer>
      <v-btn @click="toggleDrawer" text>
        Menu
        <v-icon>mdi-menu</v-icon>
      </v-btn>
    </v-app-bar>
    </div>

  </v-app>
</template>

<script>
export default {
  data() {
    return {
      isLoggedIn: false,
      user: null,
      drawer: false,
      isBarRetracted: false,
      isHeaderSticky: false,
    };
  },
  mounted() {
    window.addEventListener('scroll', this.handleScroll);
    // Initial check for the header position on mount
    this.handleScroll();
  },
  beforeDestroy() {
    window.removeEventListener('scroll', this.handleScroll);
  },
  created() {
    this.isLoggedIn = this.$auth.loggedIn;
    this.user = this.$auth.$storage.getUniversal('user');

    this.$watch('$auth.loggedIn', (loggedIn) => {
      this.isLoggedIn = loggedIn;
      this.user = this.$auth.$storage.getUniversal('user');
    });
  },
  methods: {
    handleScroll() {
      // Set a scroll threshold to change header behavior
      const scrollThreshold = 100; // Adjust as needed
      this.isHeaderSticky = window.pageYOffset > scrollThreshold;
    },
    logout() {
      this.$auth
        .logout()
        .then(() => {
          this.$auth.$storage.removeUniversal('user', null, true);

          // Check if the user is logged out
          if (!this.$auth.loggedIn) {
            if (this.$route.path !== '/') {
              this.$router.push('/');
            }
          } else {
            // Handle the case where logout was not successful
            console.error('Logout unsuccessful');
          }
        })
        .catch((error) => {
          console.error('Error during logout:', error);
          // Handle logout error if needed
        });
    },
    toggleDrawer() {
      this.drawer = !this.drawer;
      this.isBarRetracted = this.drawer;
    },
  },
};
</script>

<style lang="scss" scoped>
@import '@/assets/overhead-bar.scss';
</style>

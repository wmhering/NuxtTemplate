<template>
  <v-row justify="center" align="center">
    <v-col cols="12" sm="6" md="6">
      <v-card>
        <div class="logo py-6 d-flex justify-center">
          <CuyahogaLogo />
        </div>
        <v-card-title class="headline"></v-card-title>
        <v-card-text>
          <v-form ref="loginForm"
                  v-model="valid">
            <v-text-field v-model="username"
                          label="Name"
                          :rules="[v => !!v || 'User name is required']"
                          required
                          clearable>
              <template v-slot:prepend>
                <v-icon large>mdi-account</v-icon>
              </template>
            </v-text-field>

            <v-text-field v-model="password"
                          label="password"
                          type="password"
                          :rules="[v => !!v || 'Password is required']"
                          required
                          clearable>
              <template v-slot:prepend>
                <v-icon large>mdi-key-variant</v-icon>
              </template>
            </v-text-field>
          </v-form>
        </v-card-text>
        <v-card-actions class="px-8 pb-8 justify-center">
          <v-btn :disabled="!valid"
                 block
                 large
                 color="success"
                 :loading="loading"
                 @click="login">
            login
          </v-btn>
          <!--v-btn color="primary"
           nuxt
           to="/inspire">
      Continue
    </v-btn-->
        </v-card-actions>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
  import { mapState, mapMutations } from 'vuex'
  export default {   
    //auth: false,
    name: 'LoginPage',
    components: {
    },
    props: {
    },
    data () {
      return {
        loading: false,
        valid: false,
        username: '',
        password: '',
        emailRules: [
          v => !!v || 'E-mail is required',
          v => /.+@.+/.test(v) || 'E-mail must be valid',
        ],
      }
    },
    computed: {
      ...mapState('auth', ['loggedIn', 'user'])
    },
    watch: {
    },
    methods: {
      ...mapMutations('alert', ['info', 'success', 'error', 'warn']),
      async login() {
        this.loading = true;
        //console.log(this.$axios.defaults.baseURL);
        await this.$auth.loginWith('cookie', { data: { username: this.username, password: this.password } })
          .then(response => {
            this.$auth.$storage.setState('menuItems', response.data.menuItems);//this.$auth.user.MenuItems);
            this.$router.push('/home');
          }).catch(error => {
            this.error('Failed to login!');
            console.error(error.stack);
          }).finally(() => this.loading = false);
      }
    },
    created() {
    },
    mounted() {
      if (this.loggedIn) {
        this.$router.push('/home');
      }
    }
  }
</script>

<style>
  .custom-loader {
    animation: loader 1s infinite;
    display: flex;
  }
</style>

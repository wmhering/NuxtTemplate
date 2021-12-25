<template>
  <v-app dark>
    <Alert />
    <v-navigation-drawer v-model="drawer"
                         disable-resize-watcher
                         :mini-variant.sync="miniVariant"
                         :mini-variant-width="90"
                         expand-on-hover
                         :permanent="loggedIn"
                         @update:mini-variant="updateMiniVariant"
                         app>

      <v-list-item class="px-6">
        <v-list-item-avatar :size="miniVariant ? '80' : 200">
          <v-img src="/cuyahoga-logo.png"
                 alt="Cuyahoga"
                 max-height="200"
                 max-width="200"></v-img>
        </v-list-item-avatar>
        <v-list-item-title></v-list-item-title>
      </v-list-item>
      <v-divider class="mx-4"></v-divider>
      <v-treeview :open.sync="openNode"
                  :items="menuItems"
                  :active.sync="activeNode"
                  @update:active="setActiveNode"
                  activatable
                  active-class="primary--text"
                  item-key="Id"
                  item-text="Name"
                  item-children="Children"
                  expand-icon=""
                  open-on-click
                  return-object>
        <template v-slot:prepend="{ item, open, active }">
          <v-tooltip right
                     max-width="300"
                     :disabled="!item.Description">
            <template v-slot:activator="{ on, attrs }">
              <v-icon x-large
                      v-if="!item.Icon"
                      v-bind="attrs"
                      v-on="on">
                {{ open ? 'mdi-folder-open' : 'mdi-folder' }}
              </v-icon>
              <v-icon x-large
                      v-else
                      :style="{'color': active ? '#1976D2' : ''}"
                      v-bind="attrs"
                      v-on="on">
                {{ item.Icon }}
              </v-icon>
            </template>
            <span>{{ item.Description }}</span>
          </v-tooltip>
        </template>
      </v-treeview>

      <template v-slot:append>
        <div class="pa-4 text-center">
          <v-btn v-show="loggedIn && !miniVariant"
                 block
                 color="error"
                 large
                 @click="logout">
            logout
          </v-btn>
        </div>
      </template>
    </v-navigation-drawer>
    <v-main>
      <v-container>
        <v-row justify="center" class="text-h3 pt-2 pb-6" :style="{ color: getTitleColor }">{{ title }} {{ environmentName }}</v-row>
        <Nuxt />
      </v-container>
    </v-main>
    <v-footer :absolute="!fixed"
              app>
      <v-spacer />
      <span>&copy; {{ new Date().getFullYear() }}</span>
    </v-footer>
  </v-app>
</template>

<script>
  import { mapState, mapMutations } from 'vuex'
  export default {
    name: 'DefaultPage',
    components: {
    },
    props: {
    },
    data() {
      return {
        environmentName: 'Development',
        clipped: false,
        drawer: false,
        fixed: false,
        openNode: [],
        activeNode: [],
        menuItems: [],
        miniVariant: true,
        title: 'JFS-HET'
      }
    },
    computed: {
      ...mapState('auth', ['loggedIn', 'user']),
      getTitleColor() {
        return this.environmentName === 'Development' ? 'red' : this.environmentName === 'Production' ? 'blue' : 'yellow';
      }
    },
    watch: {
    },
    methods: {
        findMenuItem(item, id, path) {
        if ((id && item.Id === id) || (path && item.Path === path)) {
          return item;
        } else if (item.Children.length) {
          for (let child of item.Children) {
            const menuItem = this.findMenuItem(child, id, path);
            if (menuItem) {
              return menuItem;
            };
          }
        }
        return null;
      },
      updateMiniVariant(value) {
        let initOpen = [];
        if (!value && this.$auth.$storage.getLocalStorage('activeNode')) {
          let parentId = this.$auth.$storage.getLocalStorage('activeNode').ParentId;
          while (parentId) {
            const menuItem = this.findMenuItem({ Id: 0, Path: '', Children: this.menuItems }, parentId, null);
            if (menuItem) {
              initOpen.push(menuItem);
              parentId = menuItem.ParentId;
            } else {
              parentId = null;
            }
          }
        }
        this.openNode = initOpen;
      },
      setActiveNode(nodes) {
        if (nodes.length && nodes[0].Path) {
          this.$auth.$storage.setLocalStorage('activeNode', nodes[0]);
          this.$router.push(nodes[0].Path);
        } else if (this.$auth.$storage.getLocalStorage('activeNode')) {
          this.activeNode = [this.$auth.$storage.getLocalStorage('activeNode')];
        }
      },
      async logout() {
        await this.$auth.logout()
          .catch(error => console.log(error.stack))
          .finally(() => {
            this.drawer = false;
            this.openNode = [];
            this.$auth.$storage.removeLocalStorage('menuItems');
            this.$auth.$storage.removeLocalStorage('activeNode');
          });
      },
      async getEnviromentName() {
        return this.$axios.$get('/Auth/getEnviromentName')
          .then(response => {
            this.environmentName = response.environmentName;
            this.title = response.applicationName;
          })
          .catch(error => {
            this.error('Failed to get enviroment name!');
            console.log(error.stack);
          }).finally(() => { });
      }
    },
    created() {
      this.$auth.$storage.watchState('menuItems', mi => {
        this.menuItems = mi;
        if (this.menuItems.length) {
          this.$auth.$storage.setLocalStorage('menuItems', mi);
          this.activeNode = [this.menuItems[0]];
        }
      });
    },
    mounted() {
      this.getEnviromentName();

      if (this.$auth.$storage.getLocalStorage('menuItems')) {
        this.menuItems = this.$auth.$storage.getLocalStorage('menuItems');
        if (this.$route.path !== '/') {
          const menuItem = this.findMenuItem({ Id: 0, Path: '', Children: this.menuItems }, null, this.$route.path);
          this.activeNode = menuItem ? [menuItem] : [];
        } else {
          this.activeNode = [];
        }
      }    
    }
  }
</script>

<style>
  .v-treeview-node__root {
    padding-left: 0px;
    padding-right: 20px;
  }
  .v-application--is-ltr .v-treeview-node__content {
    margin-left: 0px;
  }
</style>

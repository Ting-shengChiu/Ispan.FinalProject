<template>
    <v-col cols class="d-flex justify-end">
        <div class="text-center">
            <v-menu open-on-hover>
                <template v-slot:activator="{ props }">
                    <v-row class="align-center">

                        <v-btn href="/Member" icon class="ms-auto me-5" v-bind="props">
                            <v-avatar color="grey-lighten-3" size="large">
                                <v-img :src=store.userImg :alt=store.userName></v-img>
                            </v-avatar>
                        </v-btn>
                        <h2 class="text-overline d-md-block d-none">Welcome! {{ store.userName }}</h2>
                    </v-row>

                </template>

                <v-card class="mx-auto pa-2 rounded-xl rounded-ts-0" max-width="300">
                    <v-list>
                        <v-btn color="warning" class="mt-4" @click="store.Logout()" href="/" block>
                            登出
                        </v-btn>
                        <v-list-subheader>會員專區</v-list-subheader>

                        <v-list-item v-for="(item, i) in items" :key="i" type:btn :value="item" color="primary"
                            rounded="xl">
                            <template v-slot:prepend>
                                <v-icon :icon="item.icon"></v-icon>
                            </template>

                            <v-list-item-title v-text="item.text"></v-list-item-title>
                        </v-list-item>
                    </v-list>
                </v-card>
            </v-menu>
        </div>
    </v-col>
</template>

<script setup>
import { mdiAccountOutline, mdiClock, mdiAccount, mdiFlag } from '@mdi/js';
import { computed, ref } from 'vue';
import { userStateStore } from '../../stores/UserStateStore';
const store = userStateStore();
const items = ref([
    { text: '個人化中心', icon: mdiClock },
    { text: '會員紅利', icon: mdiAccount },
    { text: '商品訂單', icon: mdiFlag },
])
</script>

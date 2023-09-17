<script setup>
import { mdiSend } from '@mdi/js';
import { ref, computed, watch } from 'vue';
import ChatMessage from '@/components/homepage/ChatMessage.vue';
import { v4 as uuidv4 } from 'uuid';
import * as signalR from "@microsoft/signalr";

const inputMessage = ref("");
const tab = ref(null);

const chatwindow = ref(null);


const hasMessage = computed(() => inputMessage.value.length > 0);

const users = ref([

]);

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();


connection.on("messageReceivedFromUser", (username, message, connectionId) => {
    const userInTabs = users.value.find(u => u.connectionId === connectionId);
    if (!userInTabs) {
        users.value.push(
            {
                username,
                connectionId,
                messages: [{
                    message,
                    isSelf: false,
                    key: uuidv4(),
                    read: tab.value === connectionId
                }]
            }
        )
        return;
    }

    userInTabs.messages.push({
        message,
        isSelf: false,
        key: uuidv4(),
        read: tab.value === connectionId
    });

    if (tab.value === null) {
        tab.value = connectionId;
    }
});

connection.on("messageReceivedFromAdmin", (userConnectionId, message) => {
    const userInTabs = users.value.find(u => u.connectionId === userConnectionId);
    userInTabs.messages.push({
        message,
        isSelf: true,
        read: true,
        key: uuidv4()
    });
})

function send(connectionId) {
    if (!hasMessage.value) return;

    connection.send("SendMessageToUser", connectionId, inputMessage.value)
        .then(() => (inputMessage.value));

    inputMessage.value = "";
}


connection
    .start()
    .catch((err) => console.log(err));

watch(tab, (newTab) => {
    const targetUser = users.value.find(u => u.connectionId === newTab);
    targetUser.messages = targetUser.messages.map(m => Object.assign({}, {...m, read: true}));
})

</script>

<template>
    <main>
        <v-card>
            <v-tabs v-model="tab" color="deep-purple-accent-4" align-tabs="center">
                <v-tab v-for="user in users" :key="user.connectionId" :value="user.connectionId">{{ user.username
                }}<v-badge :content="user.messages.filter(m => !m.read).length"
                        color="error" v-if="user.messages.filter(m => !m.read).length">&nbsp;&nbsp;&nbsp;</v-badge></v-tab>
            </v-tabs>
            <v-window v-model="tab">
                <v-window-item v-for="user in users" :key="user.connectionId" :value="user.connectionId">
                    <v-container fluid class="align-center justify-center">
                        <div class="chat-window d-flex flex-column h-100">
                            <div class="chat-header px-3 pt-2 text-h6 d-flex my-3">
                                <span>聊天室</span>
                            </div>
                            <v-divider :thickness="2"></v-divider>
                            <div class="chat-body">
                                <v-container fluid class="pa-4 px-6 pb-12 h-100 overflow-y-auto" ref="chatwindow">
                                    <ChatMessage v-for="message in user.messages" :is-self="message.isSelf"
                                        :key="message.key">
                                        {{ message.message }}
                                    </ChatMessage>
                                </v-container>
                            </div>
                            <div class="chat-footer d-flex align-center">
                                <v-text-field placeholder="請輸入訊息" variant="solo-filled" class="p-6"
                                    @keyup.enter="send(user.connectionId)" v-model="inputMessage" hide-details="auto">
                                    <template v-slot:append-inner>
                                        <v-btn :disabled="!hasMessage" :icon="mdiSend" @click="send(user.connectionId)" />
                                    </template>
                                </v-text-field>
                            </div>
                        </div>
                    </v-container>
                </v-window-item>
            </v-window>
        </v-card>
    </main>
</template>

<style scoped>
.chat-btn {
    position: fixed;
    bottom: 3vh;
    right: 3vh;
    color: white;
}

.chat-header {
    flex: 0 0 40px;
    width: 100%;
}

.chat-body {
    flex: 1 1 auto;
    overflow-y: auto;
    height: 600px;
}

.chat-footer {
    flex: 0 0 36px;
    justify-self: end;
}
</style>
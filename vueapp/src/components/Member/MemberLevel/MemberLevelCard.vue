<script setup>
import { ref, onMounted } from 'vue'
import { mdiArrowRightThin, mdiClockTimeEight } from '@mdi/js';
import axios from 'axios';
const cards = ref({
    "memberCardId": 1,
    "levelOrder": 1,
    "levelName": "腕力",
    "upgradePrice": 0,
    "upgradeOrderCount": 0,
    "timePeriod": 0,
    "gainPointRate": 0.02,
    "description": "會員等級LV1(預設) 無升級條件 永久會員 回饋點數2%"
},)

async function getCards() {
    const res = await axios.get('/api/MemberLevel/MemberCards')
    cards.value = res.data
    console.log(cards.value)
}

onMounted(() => {
    getCards()
})

</script>
<template>
    <v-card v-for="card in cards" :key="card.memberCardId" class="mx-auto" max-width="344" color="warning">
        <v-card-text>
            <v-chip class="ma-2" color="white"> 一般會員 </v-chip>
            <p class="text-h4 text--primary">Lv.{{ card.levelOrder }} {{ card.levelName }}</p>
            <v-row class="mt-2">
                <v-col cols="auto" class="align-self-end">
                    <p>可用點數:</p>
                </v-col>
                <v-col>
                    <p class="text-h3">2980pt</p>
                </v-col>
            </v-row>
        </v-card-text>
        <div class="d-flex py-1 justify-space-between">
            <v-list-item density="compact" :prepend-icon="mdiClockTimeEight">
                <v-list-item-subtitle>2022-08-01</v-list-item-subtitle>
            </v-list-item>
            <v-list-item density="compact" :prepend-icon="mdiArrowRightThin">
                <v-list-item-subtitle>2023-08-14</v-list-item-subtitle>
            </v-list-item>
        </div>
    </v-card>



    <!-- <v-card class="mx-auto" max-width="344" color="warning">
        <v-card-text>
            <v-chip class="ma-2" color="white"> 一般會員 </v-chip>
            <p class="text-h4 text--primary">Lv.1 腕力</p>
            <v-row class="mt-2">
                <v-col cols="auto" class="align-self-end">
                    <p>可用點數:</p>
                </v-col>
                <v-col>
                    <p class="text-h3">2980pt</p>
                </v-col>
            </v-row>
        </v-card-text>
        <div class="d-flex py-1 justify-space-between">
            <v-list-item density="compact" :prepend-icon="mdiClockTimeEight">
                <v-list-item-subtitle>2022-08-01</v-list-item-subtitle>
            </v-list-item>
            <v-list-item density="compact" :prepend-icon="mdiArrowRightThin">
                <v-list-item-subtitle>2023-08-14</v-list-item-subtitle>
            </v-list-item>
        </div>
    </v-card> -->
</template>
    
<style></style>
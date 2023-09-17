<script setup>
import { ref, reactive } from 'vue'
import MemberLevelCard from './MemberLevelCard.vue'
import { ElCarousel, ElCarouselItem } from 'element-plus'
const orderValuePercent = ref(50)
const orderCountPercent = ref(50)
const levelOrderValue = ref(1000)
const levelOderCount = ref(6)
const levelUpgrade = ref(33)

</script>
<template>
    <v-card variant="flat">
        <v-row class="mt-10">
            <v-col>
                <el-carousel type="card" height="280px" :autoplay="false" arrow="never">
                    <el-carousel-item v-for="item in 3" :key="item">
                        <h3 text="2xl" justify="center">
                            <MemberLevelCard></MemberLevelCard>
                        </h3>
                    </el-carousel-item>
                </el-carousel>
            </v-col>
        </v-row>
        <v-row class="mt-6">
            <v-col cols="2" class="text-center text-h6">訂單筆數</v-col>
            <v-col cols="2" class="text-center text-h6">消費金額</v-col>
        </v-row>
        <v-row>
            <v-col cols="2" class="text-center"><v-progress-circular :rotate="360" :size="130" :width="15"
                    :model-value="orderValuePercent" color="warning">
                    {{ levelOrderValue * orderValuePercent / 100 }} / {{ levelOrderValue }}
                </v-progress-circular></v-col>
            <v-col cols="2" class="text-center"><v-progress-circular :rotate="360" :size="130" :width="15"
                    :model-value="orderCountPercent" color="warning">
                    {{ levelOderCount * orderCountPercent / 100 }} / {{ levelOderCount }}
                </v-progress-circular></v-col>
        </v-row>
        <v-row class="mt-10">
            <v-col class="text-h6">
                進化條:
            </v-col>
        </v-row>
        <v-row>
            <v-col>
                <v-progress-linear v-model="levelUpgrade" color="amber" height="25">
                    <strong>{{ Math.ceil(levelUpgrade) }}%</strong>
                </v-progress-linear>
            </v-col>
        </v-row>

    </v-card>
</template>

<style scoped></style>
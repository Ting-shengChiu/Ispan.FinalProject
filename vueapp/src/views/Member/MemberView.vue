<script setup>
import { ref, computed, onMounted } from 'vue'
import { useRoute } from 'vue-router';
import { useTitle } from '@vueuse/core';
import DefaultHeader from '../../components/layouts/DefaultHeader.vue';
import DefaultFooter from '../../components/layouts/DefaultFooter.vue';
import MemberLayout from '../../components/Member/MemberInfo/MemberLayout.vue';
import MemberLevelLayout from '../../components/Member/MemberLevel/MemberLevelLayout.vue';
import MemberOrderIndex from '../../components/Member/MemberOrder/MemberOrderIndex.vue';
import MemberCourseIndex from '../../components/Member/MemberCourse/MemberCourseIndex.vue';
import MemberExpCourse from '../../components/Member/MemberCourse/MemberExpCourse.vue';
import CouponList from '../../components/Coupon/CouponList.vue';

useTitle('Bump - 會員中心');

const route = useRoute();

onMounted(() => {
    const { title, subtitle } = route.params;
    console.log(title);
    console.log(subtitle);
    tab.value = '個人化設定';
})


const profileImg = ref(['大頭貼'])
const tab = ref('個人化設定')

const cols = computed(() => {
    // const { lg, sm } = vuetify.breakpoint;
    return [3, 9]
})
const open = ref(['members'])
const options = ref([
    {
        value: 'members',
        title: '會員中心',
        subTitles: [
            '個人化設定',
            // '會員紅利',
            '會員等級'
        ]
    },
    {
        value: 'orders',
        title: '訂單中心',
        subTitles: [
            '歷史訂單',
        ]
    },
    {
        value: 'courses',
        title: '課程中心',
        subTitles: [
            '技巧課程',
            '體驗課程'
        ]
    },
    {
        value: 'coupons',
        title: '優惠券匣',
        subTitles: [
            '商品優惠券'
        ]
    }
])

</script>

<template>
    <DefaultHeader></DefaultHeader>
    <v-container class="h-screen">
        <v-row class="h-screen" no-gutters>
            <v-col :cols="cols[0]">
                <v-sheet class="my-10 ms-8">
                    <v-card class="mx-auto" width="200" variant="flat">
                        <v-list v-model:opened="open">
                            <v-list-group v-for="(option, index) in options" :key="index" :value="option.value">
                                <template v-slot:activator="{ props }">
                                    <v-list-item v-bind="props" :title="option.title" class="justify-center"></v-list-item>
                                </template>
                                <v-tabs v-model="tab" direction="vertical" color="warning">
                                    <v-tab v-for="subTitle in option.subTitles" :value="subTitle" class="justify-center">
                                        {{ subTitle }}
                                    </v-tab>
                                </v-tabs>
                            </v-list-group>
                        </v-list>
                    </v-card>
                </v-sheet>
            </v-col>
            <v-col :cols="cols[1]">
                <v-sheet>
                    <v-window v-model="tab">
                        <v-window-item :value="options[0].subTitles[1]">
                            <v-card flat>
                                <MemberLevelLayout></MemberLevelLayout>
                            </v-card>
                        </v-window-item>
                   
                        <v-window-item :value="options[0].subTitles[2]">
                            <v-card flat>
                                <MemberLevelLayout></MemberLevelLayout>
                            </v-card>
                        </v-window-item>
                   
                        <v-window-item :value="options[1].subTitles[0]">
                            <v-card flat max-width="1200" class="mh-800px overflow-y-auto">
                                <MemberOrderIndex></MemberOrderIndex>
                            </v-card>
                        </v-window-item>
                   
                        <v-window-item :value="options[3].subTitles[0]">
                            <v-card flat max-width="1200" class="mh-800px overflow-y-auto">
                                <CouponList></CouponList>
                            </v-card>
                        </v-window-item>
                   
                        <v-window-item :value="options[2].subTitles[0]">
                            <v-card flat max-width="1200" class="mh-800px overflow-y-auto">
                                <MemberCourseIndex />
                            </v-card>
                        </v-window-item>
                   
                        <v-window-item :value="options[2].subTitles[1]">
                            <v-card flat max-width="1200" class="mh-800px overflow-y-auto">
                                <MemberExpCourse />
                            </v-card>
                        </v-window-item>
                   
                        <v-window-item :value="options[0].subTitles[0]">
                            <v-card flat>
                                <MemberLayout></MemberLayout>
                            </v-card>
                        </v-window-item>
                    </v-window>
                </v-sheet>
            </v-col>
        </v-row>
    </v-container>
    <DefaultFooter></DefaultFooter>
</template>
    
<style scoped>
.mh-800px {
    max-height: 800px;
}
</style>
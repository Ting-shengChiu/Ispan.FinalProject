<script setup>
import { ref, defineProps, computed } from 'vue';
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap/dist/js/bootstrap.js"
import DailogOrderDetail from './DailogOrderDetail.vue';
import axios from 'axios';

const orderDetailData = ref()
const inquire = ref({
    orderId: 0,
    dialog: false
});

const props = defineProps(['orderData']);
const rating = ref(null)
const filteredOrderData = computed(() => {
    return props.orderData.filter(item => item.orderStatusName == '已完成');
})

async function Inquire(id) {
    inquire.value.dialog = true;
    inquire.value.orderId = id;
    try {
        const res = await axios.get(`/api/orderDetail?id=${id}`);
        const { createAt } = res.data;
        const createDate = createAt.split("T")[0];
        const createTime = createAt.split("T")[1].slice(0, 5);

        orderDetailData.value = {
            ...res.data,
            formattedCreateAt: createDate + "  " + createTime,
        };
        orderDetailData.value.details.map((g) => {
            g.rating = rating.value;
            g.userComment = '';
            return g;
        });
    } catch (err) {
        console.error(err);
    }
}

</script>
<template>
    <v-container fluid class="mx-n4">
        <table class="table custom-table-striped">
            <thead>
                <tr>
                    <th class="text-center">
                        訂單編號
                    </th>
                    <th class="text-center">
                        商品列表
                    </th>
                    <th class="text-center">
                        商品數量
                    </th>
                    <th class="text-center">
                        商品單價
                    </th>
                    <th class="text-center">
                        總價格
                    </th>
                    <th class="text-center">
                        訂單成立時間
                    </th>
                    <th class="text-center">
                        訂單狀態
                    </th>
                    <th class="text-center">
                        訂單細節
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in filteredOrderData" :key="item.orderId" class="">
                    <td align="center" valign="middle">{{ item.orderId }}</td>
                    <td valign="middle" class="margin1">
                        <p v-for="detail in item.details" :key="detail.unitPrice">{{ detail.productName }}</p>
                    </td>
                    <td align="center" valign="middle" class="margin1">
                        <p v-for="detail in item.details" :key="detail.unitPrice">{{ detail.quantity }}</p>
                    </td>
                    <td align="center" valign="middle" class="margin1">
                        <p v-for="detail in item.details" :key="detail.unitPrice">{{ detail.unitPrice }}</p>
                    </td>
                    <td align="center" valign="middle">{{ item.totalProductsPrice }}</td>
                    <td align="center" valign="middle" style="white-space: pre;">{{ item.forrmatedCreateAt }}</td>
                    <td align="center" valign="middle">{{ item.orderStatusName }}</td>
                    <td align="center" valign="middle">
                        <v-btn variant="tonal" color="orange-darken-3" @click="Inquire(item.orderId)">查詢</v-btn>
                    </td>
                </tr>
                <br>
            </tbody>
            <DailogOrderDetail :inquire="inquire" :orderDetailData="orderDetailData" v-if="orderDetailData"
                @close="() => inquire.dialog = false">
            </DailogOrderDetail>
        </table>
    </v-container>
</template>
<style scoped>
.margin1 {
    padding-top: 20px !important;
}

th {
    background-color: #dc9511 !important;
    color: rgb(255, 255, 255) !important;
}

.custom-table-striped tbody tr:nth-child(odd) td {
    background-color: #fef9f3;
}
</style>
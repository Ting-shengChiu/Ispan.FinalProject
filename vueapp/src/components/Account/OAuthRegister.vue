<script setup>
import { ref, onMounted, watch } from 'vue'
import { ElDatePicker } from 'element-plus'
import { useRoute, useRouter } from 'vue-router'
import { useRegisterStore } from '../../stores/registerStore';
import { userStateStore } from '../../stores/UserStateStore';
import axios from 'axios'
const userStore = userStateStore()
const registerStore = useRegisterStore();
const datas = registerStore.stepsData;
const route = useRoute();
const router = useRouter()

const genders = ref(['', '女性', '男性', '其他'])
const policy = ref('')
const form = ref(null)

onMounted(() => {
    datas.account = route.query.Account;
    datas.name = route.query.Name;
    datas.email = route.query.Email
    datas.thumbnail = route.query.Thumbnail;
    datas.password = route.query.Account;
    datas.confirmPassword = route.query.Account;

    console.log(datas);

})

const loginInfo = ref({
    account: "",
    password: "",
    stayLogin: false
})


async function Submit() {
    // if (!registerStore.valid) return;

    const result = await registerStore.register();
    if (result === "註冊成功!") {

        loginInfo.value.account = registerStore.stepsData.account
        loginInfo.value.password = registerStore.stepsData.confirmPassword
        await userStore.Login(loginInfo.value);

        router.push('/')
    }
}

</script>

<template>
    <v-form v-model="registerStore.valid" @formSubmit.prevent ref="form">
        <v-container fluid>
            <v-row class="justify-center">
                <span class="text-h6">第三方登入註冊</span>
            </v-row>
            <v-row class="mt-2">
                <v-alert v-if="registerStore.errorMsg" type="error" title="發生錯誤" :text="registerStore.errorMsg"
                    variant="tonal"></v-alert>
                <!-- <v-col cols="12" sm="6">
                    <v-text-field v-model="name" color="primary" label="名字" variant="underlined" required></v-text-field>
                </v-col> -->
                <v-col cols="12">
                    <v-text-field v-model="datas.nickname" color="primary" label="暱稱" variant="underlined"></v-text-field>
                </v-col>
            </v-row>
            <v-row>
                <v-col cols="12" sm="6">
                    <v-combobox v-model="datas.gender" :items="genders" density="comfortable" label="性別"
                        variant="underlined"></v-combobox>
                </v-col>
                <v-col cols="12" sm="6">
                    <span class="demonstration">生日</span>
                    <div class="block">
                        <el-date-picker v-model="datas.birthday" type="date" placeholder="2000-01-01" />
                    </div>
                </v-col>

            </v-row>
            <v-text-field v-model="datas.phoneNumber" color="primary" label="手機" placeholder="+886 914110201"
                variant="underlined"></v-text-field>

            <v-checkbox v-model="datas.dmSubscribe" label="訂閱電子報" density="compact"></v-checkbox>
            <v-checkbox v-model="policy" label="同意隱私權與政策設定" density="compact">
                <template>
                    <div>
                        同意
                        <v-tooltip location="bottom">
                            <template v-slot:activator="{ props }">
                                <a target="_blank" href="/privacy" v-bind="props" @click.stop>
                                    隱私權
                                </a>
                            </template>
                            點我看Bump客戶隱私權條款
                        </v-tooltip>
                        與
                        <v-tooltip location="bottom">
                            <template v-slot:activator="{ props }">
                                <a target="_blank" href="/terms" v-bind="props" @click.stop>
                                    政策
                                </a>
                            </template>
                            點我看Bump電子商務約定條款
                        </v-tooltip>
                        設定
                    </div>
                </template>
            </v-checkbox>
        </v-container>
        <v-btn color="warning" class="mt-7" block @click="Submit()">註冊為 Bump 會員</v-btn>
    </v-form>
</template>
<style scoped>
.block {
    text-align: center;
    border-right: solid 1px var(--el-border-color);
    flex: 1;
    width: 50px;
}

.block:last-child {
    border-right: none;
}

.demonstration {
    color: rgb(146, 146, 146);
}

a {
    text-decoration: none;
    color: orange
}
</style>

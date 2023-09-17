<script setup>
import { ref, onMounted } from 'vue'
import { mdiEyeOffOutline, mdiEyeOutline } from '@mdi/js';
import { useRoute, useRouter } from 'vue-router'
import axios from 'axios';

const route = useRoute();
const router = useRouter();
const hidePwd = ref(true)
const snackbar = ref(false)
const timeout = ref(1000)

const rules = ref({
    required: value => !!value || '必填',
    min: v => v.length >= 8 || '長度需要超過8字元',
    passwordMatch: (v) => v === form.value.password || '密碼需要輸入一樣的值',
})

const form = ref({
    "id": 0,
    "password": "",
    "confirmPassword": "",
    "confirmCode": ""
})
onMounted(() => {
    form.value.id = route.query.memberId
    form.value.confirmCode = route.query.confirmCode
    console.log(form.value)
})

const result = ref('')

async function changePwd() {
    try {
        const res = await axios.post('/api/Account/editPassword', form.value)
        snackbar.value = true
        result.value = res.data
        console.log(res.data)
        router.push('/')
    } catch (err) {
        console.log(err)
        snackbar.value = true
        result.value = err.response.data

        //網址的confirmCode怪怪的(why最後有;reset=true??) 待修
        router.push('/')

    }
}

function getColor(result) {
    return result === '密碼變更成功!' ? 'success' : 'error'
}

</script>

<template>
    <v-container>
        <v-row class="justify-center my-8">
            <span class="text-h6">更新密碼</span>
        </v-row>
        <v-snackbar v-model="snackbar" :timeout="timeout" variant="tonal" :color="getColor(result)">{{
            result }}
        </v-snackbar>

        <v-form ref="pwdForm">
            <v-row no-gutters>
                <v-text-field v-model="form.password" color="primary" label="新密碼" :rules="[rules.required, rules.min]"
                    placeholder="Enter your password" :type="hidePwd ? 'password' : 'text'"
                    :append-inner-icon="hidePwd ? mdiEyeOffOutline : mdiEyeOutline" @click:append-inner="hidePwd = !hidePwd"
                    variant="underlined"></v-text-field>
            </v-row>
            <v-row no-gutters>
                <v-text-field v-model="form.confirmPassword" color="primary" label="確認新密碼"
                    :rules="[rules.required, rules.min, rules.passwordMatch]" placeholder="Try again your password"
                    :type="hidePwd ? 'password' : 'text'" :append-inner-icon="hidePwd ? mdiEyeOffOutline : mdiEyeOutline"
                    @click:append-inner="hidePwd = !hidePwd" variant="underlined"></v-text-field>
            </v-row>
        </v-form>
        <v-btn color="warning" class="mt-4" block @click="changePwd()">
            變更密碼
        </v-btn>

        <v-row>
        </v-row>
    </v-container>
</template>

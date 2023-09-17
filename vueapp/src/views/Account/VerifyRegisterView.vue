<script setup>
import axios from 'axios';
import { ref } from 'vue'
import { useRoute } from 'vue-router';
import { useRouter } from 'vue-router';
import { userStateStore } from '../../stores/UserStateStore';
const userStore = userStateStore()
const route = useRoute();
const router = useRouter()

const data = ref({
    id: "",
    confirmCode: ""
})

const msg = ref('')
const errorMsg = ref('')

data.value.id = route.query.id
data.value.confirmCode = route.query.confirmCode

await verifyRegister()
await userStore.GetUserInfo();
userStore.setAuthentication(true);

async function verifyRegister() {
    try {
        await axios.post('api/Account/ActiveRegister', data.value)
            .then(res => {
                msg.value = res.data;
                alert(msg.value + '驗證成功!')
            })
            .then(setTimeout(() => { router.push('/') }, 2000))
    } catch (err) {
        errorMsg.value = err.response.data
        console.log(err)
    }
}
</script>

<template>
    <span>
        {{ errorMsg }}
    </span>
</template>
    
    
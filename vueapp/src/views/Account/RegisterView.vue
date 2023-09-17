<script setup>
import { computed, ref, watch, nextTick } from 'vue'
import { mdiChevronLeft, mdiChevronRight } from '@mdi/js'
import DefaultLayout from '../../components/layouts/DefaultLayout.vue'
import Register_Account from '../../components/Account/Register_Account.vue'
import Register_Img from '../../components/Account/Register_Img.vue'
import Register_Personal from '../../components/Account/Register_Personal.vue'
import AccountLayout from '../../components/Account/AccountLayout.vue';
import RegisterStep from '../../components/Account/RegisterStep.vue'
import { useRegisterStore } from '@/stores/registerStore.js'
import { userStateStore } from '../../stores/UserStateStore'
import { useRouter } from 'vue-router';
const router = useRouter()
const userStore = userStateStore()
const registerStore = useRegisterStore();
const { register } = useRegisterStore();

const form = ref(null)
const stepIndex = ref(0);
const steps = [
    {
        component: Register_Account,
        buttonText: "下一步"
    },
    {
        component: Register_Img,
        buttonText: "下一步",
    },
    {
        component: Register_Personal,
        buttonText: "創建帳號"
    }
];

function nextStep() {
    if (stepIndex.value === steps.length - 1) {
        formSubmit();
        return
    }
    stepIndex.value = (stepIndex.value + 1);
}

function prevStep() {
    stepIndex.value = (stepIndex.value - 1 + steps.length) % steps.length
}

const loginInfo = ref({
    account: "",
    password: "",
    stayLogin: false
})

async function formSubmit() {
    if (!registerStore.valid) return;

    const result = await register();
    if (result === "註冊成功!") {

        loginInfo.value.account = registerStore.stepsData.account
        loginInfo.value.password = registerStore.stepsData.confirmPassword
        await userStore.Login(loginInfo.value);

        form.value.reset();
        router.push('/')
    }
    stepIndex.value = 0
}

</script>

<template>
    <DefaultLayout>
        <AccountLayout>
            <template #content>
                <v-alert v-if="registerStore.errorMsg" type="error" title="發生錯誤" :text="registerStore.errorMsg"
                    variant="tonal"></v-alert>
                <v-form v-model="registerStore.valid" @formSubmit.prevent ref="form">
                    <component :is="steps[stepIndex].component" ref="currentStepComponent"></component>
                </v-form>
            </template>
            <template #footer>
                <div class="card-buttom">
                    <v-card-actions>
                        <v-btn v-model="preStep" color="warning" variant="text" @click="prevStep" v-if="stepIndex > 0">
                            <v-icon :icon=mdiChevronLeft end></v-icon>
                            上一步
                        </v-btn>
                        <v-spacer></v-spacer>
                        <v-btn color="warning" :variant="steps[stepIndex].buttonText === '創建帳號' ? 'flat' : 'text'"
                            @click="nextStep">
                            {{ steps[stepIndex].buttonText }}
                            <v-icon :icon=mdiChevronRight end></v-icon>
                        </v-btn>
                    </v-card-actions>
                </div>
            </template>
        </AccountLayout>
        <v-sheet class="d-flex justify-center my-6">
            <RegisterStep :active="stepIndex" @next-step="nextStep" @prev-step="prevStep" style="width: 1000px;">
            </RegisterStep>
        </v-sheet>

    </DefaultLayout>
</template>
    
<style scoped>
.card-buttom {
    position: absolute;
    bottom: 0;
    left: 0;
    width: 100%;
    padding: 16px;
}
</style>
import { defineStore } from 'pinia';
import { watch, computed, ref } from 'vue';
import { useCartStore } from './CartStore';
// import { useRouter } from 'vue-router';
import axios from 'axios';

export const userStateStore = defineStore('userStore', () => {

    axios.defaults.withCredentials = true;

    const authenticate = ref(false)
    const claims = ref([]);
    const userId = computed(() => claims.value.memberId)
    const userName = computed(() => claims.value.name)
    const userAccount = computed(() => claims.value.account)
    const userEmail = computed(() => claims.value.emailaddress)
    const userImg = computed(() => claims.value.profileImg)
    const userPhoneNumber = computed(() => claims.value.phoneNumber)
    const cart = useCartStore();
    // const router = useRouter()

    const avatar = ref(null)

    function updateAvatar(newImg) {
        claims.value.profileImg = newImg
        userImg = newImg
    }

    async function getAvatar(newImg) {
        const res = await axios.post('/api/Member/UpdateAvatar', JSON.stringify(newImg), {
            headers: {
                'Content-Type': 'application/json',
            },
        });
        return res.data
    }

    function setAuthentication(authenticated) {
        authenticate.value = authenticated
    }

    function IsLogin() {
        return axios.get('/api/Account')
    }

    // async function GetUserInfo() {
    //     try {
    //         await axios.get('/api/Account/GetMemberStatus')
    //             .then(response => {
    //                 claims.value = response.data.claims;
    //                 const newAvatar = getAvatar()
    //                 if (newAvatar) {
    //                     updateAvatar(newAvatar)
    //                 }
    //                 // console.log(claims.value)
    //             })
    //     } catch (err) {
    //         console.log(err)
    //     }
    // };

    async function GetUserInfo() {
        try {
            const res = await axios.get('/api/Account/GetMemberStatus')
            claims.value = res.data.claims;

            if (avatar.value) {
                const newAvatar = getAvatar(avatar.value)
                updateAvatar(newAvatar)
            }

        } catch (err) {
            console.log(err)
        }
    };

    function Login(data) {
        return axios.post('/api/Account/Login', data)
            .then(() => {
                setAuthentication(true)
                GetUserInfo()
            })
    }

    function Logout() {
        return axios.delete('/api/Account/Logout')
            .then(() => {
                setAuthentication(false)
                localStorage.removeItem('userStore');
                cart.items = [];
                // console.log(router)
                // router.push('/');
            })
    }


    watch([authenticate, claims], () => {
        const datas = {
            authenticate: authenticate.value,
            claims: claims.value
        }
        localStorage.setItem('userStore', JSON.stringify(datas))
        if (!authenticate.value) localStorage.removeItem('userStore')
    })

    const storeState = localStorage.getItem('userStore');
    if (storeState) {
        const state = JSON.parse(storeState);
        authenticate.value = state.authenticate;
        claims.value = state.claims
    }

    return {
        authenticate, claims,
        setAuthentication, IsLogin, GetUserInfo, Login, Logout, updateAvatar, getAvatar,
        userId, userName, userAccount, userEmail, userImg, userPhoneNumber, avatar
    }
})
export default {
    methods: {
        formatDate(dateString) {
            const date = new Date(dateString);
            // Then specify how you want your dates to be formatted
            return new Intl.DateTimeFormat('en-UK', {
                year: 'numeric',
                month: 'numeric',
                day: 'numeric'
            }).format(date);
        },
        formatDateFull(dateString) {
            const date = new Date(dateString);
            // Then specify how you want your dates to be formatted
            return new Intl.DateTimeFormat('ru', {
                year: 'numeric',
                month: 'short',
                day: 'numeric',
                hour: 'numeric',
                minute: 'numeric',
                second: 'numeric'
            }).format(date);
        },
        formatDateYear(dateString) {
            const date = new Date(dateString);
            // Then specify how you want your dates to be formatted
            return new Intl.DateTimeFormat('ru', {
                year: 'numeric'
            }).format(date);
        }
    }
}
// =============================================
// CityPoint Bookings - Rooms Page JavaScript
// Matching Home Page Interactions
// =============================================

document.addEventListener('DOMContentLoaded', function () {

    // =============================================
    // Filter Collapse Icon Rotation
    // =============================================
    const filterToggle = document.querySelector('.filter-toggle-room');
    const filterCollapse = document.getElementById('filterCollapse');

    if (filterToggle && filterCollapse) {
        filterCollapse.addEventListener('show.bs.collapse', function () {
            filterToggle.setAttribute('aria-expanded', 'true');
        });

        filterCollapse.addEventListener('hide.bs.collapse', function () {
            filterToggle.setAttribute('aria-expanded', 'false');
        });
    }

    // =============================================
    // Smooth Scroll to Results after Filter
    // =============================================
    const filterForm = document.querySelector('.filter-body-room form');

    if (filterForm) {
        filterForm.addEventListener('submit', function (e) {
            sessionStorage.setItem('scrollToResults', 'true');
        });
    }

    if (sessionStorage.getItem('scrollToResults') === 'true') {
        sessionStorage.removeItem('scrollToResults');

        setTimeout(() => {
            const resultsSection = document.querySelector('.results-card-room');
            if (resultsSection) {
                resultsSection.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        }, 100);
    }

    // =============================================
    // Confirm Delete Action
    // =============================================
    const deleteButtons = document.querySelectorAll('.btn-delete-room, .btn-delete-room-mobile');

    deleteButtons.forEach(button => {
        button.addEventListener('click', function (e) {
            const roomName = this.closest('tr')?.querySelector('.room-name-cell')?.textContent.trim() ||
                this.closest('.room-card-mobile')?.querySelector('.room-card-title-mobile')?.textContent.trim();

            const confirmed = confirm(`Are you sure you want to delete "${roomName}"?\n\nThis action cannot be undone.`);

            if (!confirmed) {
                e.preventDefault();
            }
        });
    });

    // =============================================
    // Active Filter Counter Badge
    // =============================================
    const updateActiveFiltersCount = () => {
        const form = document.querySelector('.filter-body-room form');
        if (!form) return;

        let activeCount = 0;
        const inputs = form.querySelectorAll('input[type="text"], input[type="number"], select');

        inputs.forEach(input => {
            if (input.value && input.value !== '') {
                if (input.tagName === 'SELECT' && input.value === '') {
                    return;
                }
                activeCount++;
            }
        });

        const filterToggle = document.querySelector('.filter-toggle-room');
        if (filterToggle) {
            const existingBadge = filterToggle.querySelector('.filter-count-badge');

            if (activeCount > 0) {
                if (!existingBadge) {
                    const badge = document.createElement('span');
                    badge.className = 'filter-count-badge';
                    badge.textContent = activeCount;
                    badge.style.cssText = `
                        background: linear-gradient(135deg, #3b82f6 0%, #60a5fa 100%);
                        color: white;
                        padding: 0.3rem 0.7rem;
                        border-radius: 50%;
                        font-size: 0.85rem;
                        font-weight: 700;
                        margin-left: 0.5rem;
                        min-width: 24px;
                        text-align: center;
                    `;
                    filterToggle.appendChild(badge);
                } else {
                    existingBadge.textContent = activeCount;
                }
            } else if (existingBadge) {
                existingBadge.remove();
            }
        }
    };

    updateActiveFiltersCount();

    // =============================================
    // Real-time Filter Count Update
    // =============================================
    const filterInputs = document.querySelectorAll('.filter-body-room input, .filter-body-room select');

    filterInputs.forEach(input => {
        input.addEventListener('change', updateActiveFiltersCount);
        input.addEventListener('input', updateActiveFiltersCount);
    });

    // =============================================
    // Price Range Validation
    // =============================================
    const minPriceInput = document.getElementById('minPrice');
    const maxPriceInput = document.getElementById('maxPrice');

    const validatePriceRange = () => {
        if (minPriceInput && maxPriceInput) {
            const minPrice = parseFloat(minPriceInput.value);
            const maxPrice = parseFloat(maxPriceInput.value);

            if (minPrice && maxPrice && minPrice > maxPrice) {
                maxPriceInput.setCustomValidity('Max price must be greater than min price');
                maxPriceInput.reportValidity();
            } else {
                maxPriceInput.setCustomValidity('');
            }
        }
    };

    if (minPriceInput && maxPriceInput) {
        minPriceInput.addEventListener('change', validatePriceRange);
        maxPriceInput.addEventListener('change', validatePriceRange);
    }

    // =============================================
    // Capacity Range Validation
    // =============================================
    const minCapacityInput = document.getElementById('minCapacity');
    const maxCapacityInput = document.getElementById('maxCapacity');

    const validateCapacityRange = () => {
        if (minCapacityInput && maxCapacityInput) {
            const minCapacity = parseInt(minCapacityInput.value);
            const maxCapacity = parseInt(maxCapacityInput.value);

            if (minCapacity && maxCapacity && minCapacity > maxCapacity) {
                maxCapacityInput.setCustomValidity('Max capacity must be greater than min capacity');
                maxCapacityInput.reportValidity();
            } else {
                maxCapacityInput.setCustomValidity('');
            }
        }
    };

    if (minCapacityInput && maxCapacityInput) {
        minCapacityInput.addEventListener('change', validateCapacityRange);
        maxCapacityInput.addEventListener('change', validateCapacityRange);
    }

    // =============================================
    // Search Input Clear Button
    // =============================================
    const searchInput = document.getElementById('searchString');

    if (searchInput && searchInput.value) {
        addClearButton(searchInput);
    }

    if (searchInput) {
        searchInput.addEventListener('input', function () {
            if (this.value) {
                addClearButton(this);
            } else {
                removeClearButton(this);
            }
        });
    }

    function addClearButton(input) {
        if (input.nextElementSibling?.classList.contains('clear-search-room')) return;

        const clearBtn = document.createElement('button');
        clearBtn.type = 'button';
        clearBtn.className = 'clear-search-room';
        clearBtn.innerHTML = '<i class="bi bi-x-circle-fill"></i>';
        clearBtn.style.cssText = `
            position: absolute;
            right: 10px;
            top: 50%;
            transform: translateY(-50%);
            background: none;
            border: none;
            color: #94a3b8;
            font-size: 1.2rem;
            cursor: pointer;
            padding: 0;
            transition: color 0.3s ease;
            z-index: 10;
        `;

        clearBtn.addEventListener('mouseover', function () {
            this.style.color = '#3b82f6';
        });

        clearBtn.addEventListener('mouseout', function () {
            this.style.color = '#94a3b8';
        });

        clearBtn.addEventListener('click', function () {
            input.value = '';
            input.focus();
            removeClearButton(input);
            updateActiveFiltersCount();
        });

        input.parentElement.style.position = 'relative';
        input.parentElement.appendChild(clearBtn);
        input.style.paddingRight = '2.5rem';
    }

    function removeClearButton(input) {
        const clearBtn = input.nextElementSibling;
        if (clearBtn?.classList.contains('clear-search-room')) {
            clearBtn.remove();
            input.style.paddingRight = '';
        }
    }

    // =============================================
    // Table Row Hover Effect Enhancement
    // =============================================
    const roomRows = document.querySelectorAll('.room-row');

    roomRows.forEach(row => {
        row.addEventListener('mouseenter', function () {
            this.style.transition = 'all 0.3s cubic-bezier(0.68, -0.55, 0.265, 1.55)';
        });
    });

    // =============================================
    // Ripple Effect on Buttons (FIXED VERSION)
    // =============================================
    const buttons = document.querySelectorAll('[class*="btn-"]:not(a)');

    buttons.forEach(button => {
        // Only apply to actual buttons, not links
        if (button.tagName === 'BUTTON' || button.tagName === 'INPUT') {
            button.addEventListener('click', function (e) {
                const ripple = document.createElement('span');
                const rect = this.getBoundingClientRect();
                const size = Math.max(rect.width, rect.height);
                const x = e.clientX - rect.left - size / 2;
                const y = e.clientY - rect.top - size / 2;

                ripple.style.cssText = `
                    position: absolute;
                    width: ${size}px;
                    height: ${size}px;
                    border-radius: 50%;
                    background: rgba(255, 255, 255, 0.6);
                    left: ${x}px;
                    top: ${y}px;
                    transform: scale(0);
                    animation: rippleRoom 0.6s ease-out;
                    pointer-events: none;
                    z-index: 0;
                `;

                if (getComputedStyle(this).position === 'static') {
                    this.style.position = 'relative';
                }
                this.style.overflow = 'hidden';
                this.appendChild(ripple);

                setTimeout(() => ripple.remove(), 600);
            });
        }
    });

    const rippleStyle = document.createElement('style');
    rippleStyle.textContent = `
        @keyframes rippleRoom {
            to {
                transform: scale(4);
                opacity: 0;
            }
        }
    `;
    document.head.appendChild(rippleStyle);

    // =============================================
    // Keyboard Shortcuts
    // =============================================
    document.addEventListener('keydown', function (e) {
        // Ctrl/Cmd + K to focus search
        if ((e.ctrlKey || e.metaKey) && e.key === 'k') {
            e.preventDefault();
            const searchInput = document.getElementById('searchString');
            if (searchInput) {
                searchInput.focus();
                searchInput.select();
            }
        }
    });

    // =============================================
    // Loading State for Filter Form
    // =============================================
    if (filterForm) {
        filterForm.addEventListener('submit', function () {
            const submitBtn = this.querySelector('button[type="submit"]');
            if (submitBtn) {
                const originalHTML = submitBtn.innerHTML;
                submitBtn.disabled = true;
                submitBtn.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Loading...';
            }
        });
    }

    // =============================================
    // Scroll Progress Indicator (like home page)
    // =============================================
    const createScrollIndicator = () => {
        const scrollIndicator = document.createElement('div');
        scrollIndicator.className = 'scroll-progress-room';
        scrollIndicator.style.cssText = `
            position: fixed;
            top: 0;
            left: 0;
            height: 4px;
            background: linear-gradient(90deg, #3b82f6, #60a5fa);
            width: 0%;
            z-index: 9999;
            transition: width 0.1s ease;
            border-radius: 0 2px 2px 0;
        `;
        document.body.appendChild(scrollIndicator);

        window.addEventListener('scroll', () => {
            const windowHeight = document.documentElement.scrollHeight - document.documentElement.clientHeight;
            const scrolled = (window.pageYOffset / windowHeight) * 100;
            scrollIndicator.style.width = scrolled + '%';
        });
    };

    createScrollIndicator();

    // =============================================
    // Console Welcome Message
    // =============================================
    console.log('%c CityPoint Rooms Page Loaded ',
        'background: linear-gradient(135deg, #1e3a8a 0%, #3b82f6 100%); color: white; font-size: 16px; padding: 8px 16px; border-radius: 8px;');
    console.log('%c Use Ctrl/Cmd + K to quickly focus the search box',
        'color: #3b82f6; font-size: 12px;');

});

// =============================================
// Export utility functions
// =============================================
window.RoomsPage = {
    clearAllFilters: () => {
        const form = document.querySelector('.filter-body-room form');
        if (form) {
            form.reset();
            form.submit();
        }
    },

    focusSearch: () => {
        const searchInput = document.getElementById('searchString');
        if (searchInput) {
            searchInput.focus();
            searchInput.select();
        }
    },

    scrollToTop: () => {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    }
};
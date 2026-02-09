// =============================================
// CityPoint Bookings - Home Page JavaScript
// =============================================

document.addEventListener('DOMContentLoaded', function () {

    // Initialize AOS (Animate On Scroll) Library
    AOS.init({
        duration: 800,
        easing: 'ease-in-out',
        once: true,
        offset: 100
    });

    // =============================================
    // Smooth Scroll for Anchor Links
    // =============================================
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });

    // =============================================
    // Counter Animation for Stats Section
    // =============================================
    const counterElements = document.querySelectorAll('.stat-number');
    let countersAnimated = false;

    function animateCounters() {
        counterElements.forEach(counter => {
            const target = parseInt(counter.getAttribute('data-count'));
            const duration = 2000; // 2 seconds
            const increment = target / (duration / 16); // 60fps
            let current = 0;

            const updateCounter = () => {
                current += increment;
                if (current < target) {
                    counter.textContent = Math.floor(current);
                    requestAnimationFrame(updateCounter);
                } else {
                    counter.textContent = target;
                }
            };

            updateCounter();
        });
        countersAnimated = true;
    }

    // Intersection Observer for Stats Animation
    const statsSection = document.querySelector('.stats-section');
    if (statsSection) {
        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting && !countersAnimated) {
                    animateCounters();
                }
            });
        }, {
            threshold: 0.5
        });

        observer.observe(statsSection);
    }

    // =============================================
    // Parallax Effect for Hero Section
    // =============================================
    const heroSection = document.querySelector('.hero-section');

    window.addEventListener('scroll', () => {
        const scrolled = window.pageYOffset;
        if (heroSection && scrolled < window.innerHeight) {
            heroSection.style.transform = `translateY(${scrolled * 0.5}px)`;
        }
    });

    // =============================================
    // Add Floating Animation to Room Cards
    // =============================================
    const roomCards = document.querySelectorAll('.room-card');

    roomCards.forEach((card, index) => {
        card.style.animation = `float 3s ease-in-out infinite`;
        card.style.animationDelay = `${index * 0.2}s`;
    });

    // Add CSS for floating animation
    const style = document.createElement('style');
    style.textContent = `
        @keyframes float {
            0%, 100% {
                transform: translateY(0px);
            }
            50% {
                transform: translateY(-10px);
            }
        }
        
        .room-card:hover {
            animation: none;
        }
    `;
    document.head.appendChild(style);

    // =============================================
    // Feature Cards Hover Effect Enhancement
    // =============================================
    const featureCards = document.querySelectorAll('.feature-card');

    featureCards.forEach(card => {
        card.addEventListener('mouseenter', function () {
            this.style.transition = 'all 0.4s cubic-bezier(0.68, -0.55, 0.265, 1.55)';
        });
    });

    // =============================================
    // Scroll Progress Indicator
    // =============================================
    const createScrollIndicator = () => {
        const scrollIndicator = document.createElement('div');
        scrollIndicator.className = 'scroll-progress';
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
    // Add Loading Animation
    // =============================================
    window.addEventListener('load', () => {
        document.body.style.opacity = '0';
        setTimeout(() => {
            document.body.style.transition = 'opacity 0.5s ease';
            document.body.style.opacity = '1';
        }, 100);
    });

    // =============================================
    // Dynamic Greeting Based on Time
    // =============================================
    const updateGreeting = () => {
        const hour = new Date().getHours();
        const heroSubtitle = document.querySelector('.hero-subtitle');

        if (heroSubtitle) {
            let greeting = 'Premium room hire solutions for your business needs';

            if (hour < 12) {
                greeting = 'Good morning! ' + greeting;
            } else if (hour < 18) {
                greeting = 'Good afternoon! ' + greeting;
            } else {
                greeting = 'Good evening! ' + greeting;
            }

            // Don't override if the greeting is already custom
            if (!heroSubtitle.textContent.includes('Good')) {
                // Keep original text for now
            }
        }
    };

    // =============================================
    // Add Ripple Effect to Buttons
    // =============================================
    const buttons = document.querySelectorAll('[class*="btn-"]');

    buttons.forEach(button => {
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
                animation: ripple 0.6s ease-out;
                pointer-events: none;
            `;

            this.style.position = 'relative';
            this.style.overflow = 'hidden';
            this.appendChild(ripple);

            setTimeout(() => ripple.remove(), 600);
        });
    });

    // Add ripple animation CSS
    const rippleStyle = document.createElement('style');
    rippleStyle.textContent = `
        @keyframes ripple {
            to {
                transform: scale(4);
                opacity: 0;
            }
        }
    `;
    document.head.appendChild(rippleStyle);

    // =============================================
    // Navbar Color Change on Scroll (if navbar exists)
    // =============================================
    const navbar = document.querySelector('nav');

    if (navbar) {
        window.addEventListener('scroll', () => {
            if (window.scrollY > 100) {
                navbar.style.background = 'rgba(255, 255, 255, 0.95)';
                navbar.style.boxShadow = '0 2px 8px rgba(30, 58, 138, 0.1)';
                navbar.style.backdropFilter = 'blur(10px)';
            } else {
                navbar.style.background = 'transparent';
                navbar.style.boxShadow = 'none';
            }
        });
    }

    // =============================================
    // Performance Optimization - Lazy Load Images
    // =============================================
    const lazyLoadImages = () => {
        const images = document.querySelectorAll('img[data-src]');
        const imageObserver = new IntersectionObserver((entries, observer) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    const img = entry.target;
                    img.src = img.dataset.src;
                    img.removeAttribute('data-src');
                    observer.unobserve(img);
                }
            });
        });

        images.forEach(img => imageObserver.observe(img));
    };

    lazyLoadImages();

    // =============================================
    // Console Welcome Message
    // =============================================
    console.log('%c Welcome to CityPoint Bookings! ',
        'background: linear-gradient(135deg, #1e3a8a 0%, #3b82f6 100%); color: white; font-size: 20px; padding: 10px 20px; border-radius: 10px;');
    console.log('%c Built with ❤️ for premium room hire experiences',
        'color: #3b82f6; font-size: 14px;');

    // =============================================
    // Accessibility - Skip to Content Link
    // =============================================
    const createSkipLink = () => {
        const skipLink = document.createElement('a');
        skipLink.href = '#main-content';
        skipLink.textContent = 'Skip to main content';
        skipLink.className = 'skip-link';
        skipLink.style.cssText = `
            position: absolute;
            top: -40px;
            left: 0;
            background: #1e3a8a;
            color: white;
            padding: 8px 16px;
            text-decoration: none;
            z-index: 10000;
            border-radius: 0 0 8px 0;
        `;

        skipLink.addEventListener('focus', function () {
            this.style.top = '0';
        });

        skipLink.addEventListener('blur', function () {
            this.style.top = '-40px';
        });

        document.body.insertBefore(skipLink, document.body.firstChild);
    };

    createSkipLink();

    // =============================================
    // Add Main Content ID for Accessibility
    // =============================================
    const mainContent = document.querySelector('.features-section');
    if (mainContent) {
        mainContent.id = 'main-content';
    }

});

// =============================================
// Export functions for use in other scripts
// =============================================
window.CityPointBookings = {
    scrollToTop: () => {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    },

    refreshAOS: () => {
        if (typeof AOS !== 'undefined') {
            AOS.refresh();
        }
    }
};
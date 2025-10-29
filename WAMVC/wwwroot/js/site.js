// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// ===== ARTESANÍAS WAMVC - ENHANCED UI INTERACTIONS =====

document.addEventListener('DOMContentLoaded', function() {
    
    // ===== SMOOTH LOADING ANIMATION =====
    const body = document.body;
    body.style.opacity = '0';
    setTimeout(() => {
    body.style.transition = 'opacity 0.5s ease-in-out';
        body.style.opacity = '1';
    }, 100);

    // ===== NAVBAR SCROLL EFFECT =====
    const navbar = document.querySelector('.navbar');
    if (navbar) {
    window.addEventListener('scroll', function() {
 if (window.scrollY > 50) {
       navbar.style.background = 'rgba(255, 255, 255, 0.98)';
    navbar.style.boxShadow = '0 4px 20px rgba(0, 0, 0, 0.1)';
      } else {
 navbar.style.background = 'rgba(255, 255, 255, 0.95)';
      navbar.style.boxShadow = '0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1)';
   }
        });
    }

    // ===== CARD HOVER EFFECTS =====
    const cards = document.querySelectorAll('.card');
    cards.forEach(card => {
        card.addEventListener('mouseenter', function() {
          this.style.transform = 'translateY(-8px) scale(1.02)';
      this.style.boxShadow = '0 20px 25px -5px rgba(0, 0, 0, 0.15), 0 10px 10px -5px rgba(0, 0, 0, 0.04)';
        });
        
        card.addEventListener('mouseleave', function() {
            this.style.transform = 'translateY(0) scale(1)';
            this.style.boxShadow = '0 10px 15px -3px rgb(0 0 0 / 0.1), 0 4px 6px -4px rgb(0 0 0 / 0.1)';
      });
    });

    // ===== BUTTON RIPPLE EFFECT =====
    const buttons = document.querySelectorAll('.btn');
    buttons.forEach(button => {
        button.addEventListener('click', function(e) {
    const ripple = document.createElement('span');
       const rect = this.getBoundingClientRect();
            const size = Math.max(rect.width, rect.height);
         const x = e.clientX - rect.left - size / 2;
   const y = e.clientY - rect.top - size / 2;
            
       ripple.style.position = 'absolute';
 ripple.style.width = ripple.style.height = size + 'px';
            ripple.style.left = x + 'px';
            ripple.style.top = y + 'px';
    ripple.style.background = 'rgba(255, 255, 255, 0.4)';
  ripple.style.borderRadius = '50%';
            ripple.style.transform = 'scale(0)';
            ripple.style.animation = 'ripple 0.6s linear';
            ripple.style.pointerEvents = 'none';
            
            this.appendChild(ripple);
    
     setTimeout(() => {
              ripple.remove();
      }, 600);
     });
    });

    // ===== FORM ENHANCEMENTS =====
    const formControls = document.querySelectorAll('.form-control, .form-select');
    formControls.forEach(control => {
        // Floating label effect
        control.addEventListener('focus', function() {
   this.style.transform = 'translateY(-2px)';
            this.style.boxShadow = '0 0 0 0.2rem rgba(99, 102, 241, 0.25)';
        });
        
        control.addEventListener('blur', function() {
            this.style.transform = 'translateY(0)';
        });
    
      // Input validation visual feedback
    control.addEventListener('input', function() {
            if (this.checkValidity()) {
    this.style.borderColor = '#10b981';
  this.style.boxShadow = '0 0 0 0.2rem rgba(16, 185, 129, 0.25)';
} else if (this.value.length > 0) {
   this.style.borderColor = '#ef4444';
                this.style.boxShadow = '0 0 0 0.2rem rgba(239, 68, 68, 0.25)';
            } else {
     this.style.borderColor = '#e5e7eb';
          this.style.boxShadow = '';
   }
        });
    });

    // ===== NOTIFICATION TOAST =====
    window.showToast = function(message, type = 'info') {
        const toast = document.createElement('div');
        toast.className = `toast-notification toast-${type}`;
    toast.innerHTML = `
       <div class="toast-content">
      <div class="toast-icon">
  ${getToastIcon(type)}
     </div>
         <div class="toast-message">${message}</div>
            <button class="toast-close" onclick="this.parentElement.parentElement.remove()">
   <i class="bi bi-x"></i>
         </button>
            </div>
        `;
     
        // Add styles
      toast.style.cssText = `
            position: fixed;
          top: 20px;
 right: 20px;
      background: white;
          border-radius: 12px;
   box-shadow: 0 10px 25px rgba(0, 0, 0, 0.15);
padding: 1rem;
            z-index: 9999;
         transform: translateX(400px);
 transition: all 0.3s ease;
            border-left: 4px solid ${getToastColor(type)};
     `;
      
        document.body.appendChild(toast);
        
        // Animate in
        setTimeout(() => {
          toast.style.transform = 'translateX(0)';
        }, 100);
    
    // Auto remove
    setTimeout(() => {
     toast.style.transform = 'translateX(400px)';
          setTimeout(() => toast.remove(), 300);
        }, 4000);
    };

    function getToastIcon(type) {
        const icons = {
       success: '<i class="bi bi-check-circle-fill text-success"></i>',
            error: '<i class="bi bi-exclamation-triangle-fill text-danger"></i>',
            warning: '<i class="bi bi-exclamation-circle-fill text-warning"></i>',
      info: '<i class="bi bi-info-circle-fill text-info"></i>'
   };
      return icons[type] || icons.info;
    }

    function getToastColor(type) {
        const colors = {
   success: '#10b981',
            error: '#ef4444',
 warning: '#f59e0b',
  info: '#06b6d4'
   };
        return colors[type] || colors.info;
    }

    // ===== LOADING STATES =====
window.showLoading = function(button) {
        const originalText = button.innerHTML;
    button.innerHTML = '<i class="bi bi-arrow-clockwise spin"></i> Cargando...';
        button.disabled = true;
  
        return function() {
            button.innerHTML = originalText;
            button.disabled = false;
    };
    };

    // ===== SMOOTH SCROLL FOR ANCHOR LINKS =====
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

    // ===== STATS COUNTER ANIMATION =====
 const statNumbers = document.querySelectorAll('.stat-number');
 statNumbers.forEach(stat => {
        const targetValue = parseInt(stat.textContent.replace(/\D/g, ''));
    if (targetValue > 0) {
    animateCounter(stat, 0, targetValue, 1500);
        }
    });

    function animateCounter(element, start, end, duration) {
        let startTime = null;
  const originalText = element.textContent;
const prefix = originalText.replace(/[\d,]/g, '');
      
        function step(currentTime) {
 if (!startTime) startTime = currentTime;
       const progress = Math.min((currentTime - startTime) / duration, 1);
    const current = Math.floor(progress * (end - start) + start);
        element.textContent = prefix + current.toLocaleString();
            
      if (progress < 1) {
        requestAnimationFrame(step);
    }
        }
        
    requestAnimationFrame(step);
    }

// ===== INTERSECTION OBSERVER FOR ANIMATIONS =====
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };

    const observer = new IntersectionObserver((entries) => {
     entries.forEach(entry => {
     if (entry.isIntersecting) {
         entry.target.style.animation = 'fadeInUp 0.6s ease-out forwards';
            }
  });
    }, observerOptions);

    // Observe cards for animation
    document.querySelectorAll('.card, .stat-card, .action-card').forEach(card => {
        card.style.opacity = '0';
        card.style.transform = 'translateY(30px)';
        observer.observe(card);
    });

    // ===== THEME TOGGLE (Optional Enhancement) =====
    const themeToggle = document.getElementById('theme-toggle');
    if (themeToggle) {
    themeToggle.addEventListener('click', function() {
            document.body.classList.toggle('dark-theme');
   const isDark = document.body.classList.contains('dark-theme');
            localStorage.setItem('theme', isDark ? 'dark' : 'light');
      
         // Update icon
            const icon = this.querySelector('i');
 icon.className = isDark ? 'bi bi-sun' : 'bi bi-moon';
        });

    // Load saved theme
        const savedTheme = localStorage.getItem('theme');
        if (savedTheme === 'dark') {
       document.body.classList.add('dark-theme');
   themeToggle.querySelector('i').className = 'bi bi-sun';
        }
    }

    // ===== TABLE ENHANCEMENTS =====
    const tables = document.querySelectorAll('.table');
    tables.forEach(table => {
        const rows = table.querySelectorAll('tbody tr');
        rows.forEach((row, index) => {
            row.style.animationDelay = `${index * 0.1}s`;
  row.classList.add('table-row-animated');
        });
    });

    console.log('🎨 WAMVC UI Enhanced - Ready!');
});

// ===== CSS ANIMATIONS =====
const style = document.createElement('style');
style.textContent = `
    @keyframes ripple {
        to {
    transform: scale(4);
  opacity: 0;
        }
    }
    
    @keyframes spin {
        0% { transform: rotate(0deg); }
        100% { transform: rotate(360deg); }
    }
    
    .spin {
        animation: spin 1s linear infinite;
    }
    
    @keyframes fadeInUp {
        from {
        opacity: 0;
          transform: translateY(30px);
  }
        to {
     opacity: 1;
    transform: translateY(0);
        }
    }
    
  .table-row-animated {
        animation: fadeInUp 0.5s ease-out forwards;
        opacity: 0;
        transform: translateY(20px);
    }
    
    .toast-content {
        display: flex;
        align-items: center;
        gap: 12px;
    }
 
    .toast-close {
        background: none;
        border: none;
        font-size: 1.2rem;
        cursor: pointer;
    padding: 0;
        margin-left: auto;
      opacity: 0.6;
        transition: opacity 0.3s ease;
    }
    
    .toast-close:hover {
        opacity: 1;
    }
    
    /* Dark theme styles */
    .dark-theme {
        --primary-color: #818cf8;
        --dark-color: #f9fafb;
 background: #1f2937 !important;
  color: #f9fafb !important;
    }
    
    .dark-theme .container {
        background: rgba(31, 41, 55, 0.95) !important;
        color: #f9fafb !important;
    }
    
    .dark-theme .card {
   background: #374151 !important;
        color: #f9fafb !important;
    }
    
    .dark-theme .navbar {
        background: rgba(31, 41, 55, 0.95) !important;
    }
    
    .dark-theme .nav-link {
        color: #d1d5db !important;
    }
`;
document.head.appendChild(style);

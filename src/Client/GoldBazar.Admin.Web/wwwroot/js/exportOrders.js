// PDF export helper using html2canvas and jsPDF
(function (global) {
    /**
     * Capture an element by its ID and save as a PDF snapshot.
     * @param {string} elementId - The DOM element ID to capture.
     * @param {string} [filename='export.pdf'] - The desired PDF file name.
     */
    async function exportToPdfById(elementId, filename = 'export.pdf') {
        const el = document.getElementById(elementId);
        if (!el) {
            console.error(`[exportToPdfById] Element not found: ${elementId}`);
            return;
        }
        try {
            // Render element to canvas
            const canvas = await html2canvas(el, { scale: 2 });
            const imgData = canvas.toDataURL('image/png');

            // Initialize PDF
            const { jsPDF } = window.jspdf;
            const pdf = new jsPDF({ orientation: 'landscape', unit: 'pt', format: 'a4' });
            const pdfW = pdf.internal.pageSize.getWidth();
            const pdfH = pdf.internal.pageSize.getHeight();
            const imgW = canvas.width;
            const imgH = canvas.height;
            const ratio = Math.min(pdfW / imgW, pdfH / imgH);

            // Add image and save
            pdf.addImage(imgData, 'PNG', 0, 0, imgW * ratio, imgH * ratio);
            pdf.save(filename);
        } catch (err) {
            console.error('[exportToPdfById] Error exporting PDF', err);
        }
    }

    // Expose globally for Blazor interop
    global.exportToPdfById = exportToPdfById;
})(window);
